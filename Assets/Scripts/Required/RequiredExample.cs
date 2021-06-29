using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulFlare.RequiredVariables;
using System;

public class RequiredExample : MonoBehaviour {
	/*[Required(GameObjectType.PREFAB, new Type[] { typeof(Camera), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample), typeof(RequiredExample) })]
	public GameObject prefab11;
	[Required(GameObjectType.PREFAB, new Type[] { typeof(Camera), typeof(RequiredExample), typeof(RequiredExample) })]
	public GameObject prefab3;
	[Required(GameObjectType.PREFAB, new Type[] { typeof(Camera), typeof(RequiredExample) })]
	public GameObject prefab2;
	[Required(GameObjectType.SCENE, typeof(RequiredExample))]
	public GameObject prefabCamera;*/
	[Required(typeof(Camera))]
	public GameObject Camera;
	[Required(GameObjectType.PREFAB)]
	public GameObject Prefab;
	[Required(GameObjectType.INSTANCE)]
	public GameObject instance;
	[Required(GameObjectType.SCENE)]
	public GameObject scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
