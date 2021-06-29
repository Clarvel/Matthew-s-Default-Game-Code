using UnityEngine;
using SoulFlare.ScriptUpdater;

public class TogglableButton : UpdatableScript {
	public bool enable;
	public string axis;

	bool toggled;

	public TogglableButton(string axis, bool enabled = false) : base() {
		this.axis = axis;
		enable = enabled;
		toggled = false;
	}

	public bool GetButton() {
		if(enable) {
			return toggled;
		} else {
			return Input.GetButton(axis);
		}
	}

	public override void Update() {
		if(enable && Input.GetButtonDown(axis)) {
			toggled = !toggled;
		}
	}
}
