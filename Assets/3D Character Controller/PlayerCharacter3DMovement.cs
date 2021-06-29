using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulFlare;
using SoulFlare.Extensions;
using SoulFlare.Extensions.Events;
using UnityEngine.Events;

public enum MovementState { CONTROLLED, RAGDOLL, UPRIGHT, CROUCH, HOVER, FLIGHT, FALLING}
public enum TeeterState { STABLE, FORWARD, BACKWARD, UNSTABLE}

[System.Serializable] public class MoveStateEvent : UnityEvent<MovementState> { }

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public class PlayerCharacter3DMovement : MonoBehaviour {
	//public string animationFloatname;
	new public Camera camera;

	[Header("Upright")]
	[SerializeField] float uprightMoveSpeed = 10f;
	[SerializeField] float uprightTurnSpeed = 180f;

	[Header("Crouch")]
	[SerializeField] float crouchMoveSpeed = 10f;
	[SerializeField] float crouchTurnSpeed = 180f;

	[Header("Hover")]
	[SerializeField] float hoverMoveSpeed = 10f;
	[SerializeField] float hoverTurnSpeed = 180f;

	[Header("Flight")]
	[SerializeField] float flightMoveSpeed = 10f;
	[SerializeField] float flightTurnSpeed = 180f;

	[Header("Forces")]
	[SerializeField] float flapForce = 20f;
	[SerializeField] float boostedFlapForce = 50f;
	[SerializeField] float rollForce = 20f;

	[Header("Thresholds")]
	[SerializeField] float flyingSpeedThreshold = 1f;
	[SerializeField] float fallingSpeedThreshold = -1f;
	[SerializeField] float zeroSpeedThreshold = 0.05f;
	[SerializeField] float groundedThreshold = 0.1f;

	[Header("Raycasts")]
	[SerializeField] float floorcastLength = 1.6f;
	[SerializeField] float armcastLength = 1.6f;


	public FloatEvent OnLand = new FloatEvent();
	public FloatEvent OnJump = new FloatEvent();
	public Vector2Event OnMove = new Vector2Event();
	public MoveStateEvent OnMoveStatechange = new MoveStateEvent();

	[HideInInspector]
	public MovementState MovementState { get; protected set; }
	[HideInInspector]
	public float FloorAvgHeight { get; protected set; }
	[HideInInspector]
	public Vector3 FloorAvgNormal { get; protected set; }
	[HideInInspector]
	public TeeterState TeeterState { get; protected set; }
	[HideInInspector]
	public bool IsGrounded { get;  protected set; }


	Rigidbody rb;
	Animator an;
	CapsuleCollider cc;

	Vector3 move;
	Vector3 turn;
	Vector3 push;

	//float inputAmount = 0f; // used for animating?
	float _ccHeight;
	float _ccCenter;

	float getColliderBaseHeightFromTransform() {
		if(Mathf.Approximately(cc.height, 0f)) {
			return -cc.radius;
		}
		return cc.center.y - cc.height / 2f;
	}

	// Use this for initialization
	void Awake() {
		an = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CapsuleCollider>();

		camera = camera == null ? Camera.main : camera;

		rb.constraints = RigidbodyConstraints.FreezeRotation;
		rb.useGravity = false;
		rb.isKinematic = false;
		turn = rb.rotation.eulerAngles;
		_ccHeight = cc.height;
		_ccCenter = cc.center.y;
		MovementState = MovementState.UPRIGHT;
	}

	void Update() {
		FindFloorDist();
		float movespeed = 0f;
		float turnspeed = 0f;

		switch(MovementState) {
			case MovementState.RAGDOLL:
				if(Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f && IsGrounded) {
					// TODO set animator
					EnterCrouch();
				}
				break;
			case MovementState.CROUCH:
				// TODO prevent walking off ledges
				if(!Input.GetButton("Crouch")) {
					// TODO set animator
					ExitCrouch();
					MovementState = MovementState.UPRIGHT;
				}else{
					if(Input.GetButtonDown("Jump")) {
						Debug.Log(rb.velocity.x + " " + rb.velocity.y + " " + rb.velocity.z + " " + rb.velocity.magnitude);
						if(Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f) {
							rb.AddForce(Vector3.up * boostedFlapForce, ForceMode.VelocityChange);
						} else {
							rb.AddForce(transform.forward * rollForce, ForceMode.VelocityChange);
						}
					}
					movespeed = crouchMoveSpeed;
					turnspeed = crouchTurnSpeed;
				}
				break;
			case MovementState.UPRIGHT:
				if(!IsGrounded && rb.velocity.y < fallingSpeedThreshold) {
					// TODO set animator
					MovementState = MovementState.FALLING;
				} else if(Input.GetButtonDown("Crouch") && Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f) {
					// TODO set animator
					EnterCrouch();
				} else{
					if(Input.GetButtonDown("Jump")) {
						if(rb.velocity.magnitude < flyingSpeedThreshold) {
							// TODO set animator and collider
							rb.AddForce(Vector3.up * flapForce, ForceMode.VelocityChange);
							MovementState = MovementState.HOVER;
						} else {
							// TODO set animator and collider
							rb.AddForce(transform.forward * flapForce, ForceMode.VelocityChange);
							MovementState = MovementState.FLIGHT;
						}
					}
					movespeed = uprightMoveSpeed;
					turnspeed = uprightTurnSpeed;
				}
				break;
			case MovementState.FLIGHT:
				if(rb.velocity.magnitude < flyingSpeedThreshold) {
					// TODO set animator and collider
					MovementState = MovementState.HOVER;
				}else{
					if(Input.GetButtonDown("Jump")) {
						rb.AddForce(Vector3.up * flapForce, ForceMode.VelocityChange);
					}
					movespeed = flightMoveSpeed;
					turnspeed = flightTurnSpeed;
				}
				break;
			case MovementState.FALLING:
				if(IsGrounded) {
					EnterCrouch();
				}
				break;
			case MovementState.HOVER:
				if(IsGrounded) {
					MovementState = MovementState.UPRIGHT;
				} else if(rb.velocity.y < fallingSpeedThreshold) {
					// TODO set animator
					ExitCrouch();
					MovementState = MovementState.FALLING;
				} else {
					if(Input.GetButtonDown("Jump")) {
						rb.AddForce(Vector3.up * flapForce, ForceMode.VelocityChange);
					}
					movespeed = hoverMoveSpeed;
					turnspeed = hoverTurnSpeed;
				}
				break;
			default:
				break;
		}
		ApplyInput(movespeed, turnspeed);

		// set animation values
		an.SetFloat("Move", move.z);
		an.SetFloat("Turn", move.x);
		//an.SetFloat(animationFloatname, inputAmount, 0.2f, Time.deltaTime); // handle animation blendtree for walking
	}

	void FixedUpdate() {
		rb.MoveRotation(Quaternion.Euler(turn));
		rb.MovePosition(move);
		rb.AddForce(push, ForceMode.Acceleration);
	}

	void EnterCrouch() {
		rb.velocity = Vector3.zero;
		cc.height = 0f;
		cc.center = Vector3.up * cc.radius;
		an.SetBool("Crouch", true);
		MovementState = MovementState.CROUCH;
	}

	void ExitCrouch() {
		cc.height = _ccHeight;
		cc.center = Vector3.up * _ccCenter;
		an.SetBool("Crouch", false);
	}

	void ApplyInput(float movespeed, float turnspeed) {
		Vector2 result = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if(result.sqrMagnitude > 1f) result.Normalize();
		Vector3 movement = Quaternion.Euler(0, camera.transform.eulerAngles.y - transform.eulerAngles.y, 0) * new Vector3(result.x, 0, result.y);

		float forwardAmount = Mathf.Clamp01(movement.z);
		movement.x += (forwardAmount - movement.z) * Mathf.Sign(movement.x);

		float rad = Mathf.Deg2Rad * transform.eulerAngles.y;

		move = transform.position + Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward * forwardAmount * Time.fixedDeltaTime * movespeed;
		turn += Vector3.up * movement.x * 90f * Time.fixedDeltaTime * turnspeed; // turn overwrites each time, so need to track total turn amount
		push = Physics.gravity;
		//return new Vector2(forwardAmount, movement.x);
	}

	void FindFloorDist() {
		FloorAvgHeight = 0f;
		FloorAvgNormal = Vector3.zero;
		TeeterState = TeeterState.UNSTABLE;
		IsGrounded = false;

		int count = 0;
		Vector3 rayOrigin = transform.position + (transform.up.y >= 0f ? transform.up : -transform.up) * cc.radius;
		//rayOrigin -= Vector3.up * floorOffset;
		Vector3 offset = transform.TransformDirection(0f, 0f, cc.radius);
		RaycastHit hit;

		Debug.DrawLine(transform.position, rayOrigin, Color.blue);

		if(RayCast(rayOrigin + offset, Vector3.down, floorcastLength, out hit)) { // forward raycast
			FloorAvgHeight += hit.distance - cc.radius;
			FloorAvgNormal += hit.normal;
			count++;
		}

		bool backward = false;
		if(RayCast(rayOrigin - offset, Vector3.down, floorcastLength, out hit)) { // backward raycast
			FloorAvgHeight += hit.distance - cc.radius;
			FloorAvgNormal += hit.normal;
			count++;
			backward = true;
		}

		if(RayCast(rayOrigin + Vector3.down * cc.radius, Vector3.down, floorcastLength, out hit)) { // center raycast
			FloorAvgHeight += hit.distance;
			FloorAvgNormal += hit.normal;
			count++;
			if(count >= 3) {
				TeeterState = TeeterState.STABLE;
			}else if(count == 2) {
				if(backward) {
					TeeterState = TeeterState.BACKWARD;
				} else {
					TeeterState = TeeterState.FORWARD;
				}
			}
		}

		if(count > 0) {
			FloorAvgHeight /= count;
			FloorAvgNormal /= count;
			if(FloorAvgHeight < groundedThreshold) {
				IsGrounded = true;
			}
		} else {
			FloorAvgHeight = float.PositiveInfinity;
		}
	}

	bool RayCast(Vector3 pos, Vector3 dir, float length, out RaycastHit hit) {
		if(Physics.Raycast(pos, dir, out hit, length)) {
			Debug.DrawLine(pos, hit.point, Color.green);
			return true;
		}
		Debug.DrawLine(pos, pos + Vector3.down * length, Color.red);
		return false;
	}
}