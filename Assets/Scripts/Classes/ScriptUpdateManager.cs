using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoulFlare.Singleton;

namespace SoulFlare.ScriptUpdater {

	public class UpdatableScript {
		public UpdatableScript() {
			ScriptUpdateManager.Instance.Register(this);
		}
		public virtual void Update() { }
		public virtual void LateUpdate() { }
		public virtual void FixedUpdate() { }
	}

	public enum UpdateType { UPDATE, LATE_UPDATE, FIXED_UPDATE }

	public class ScriptUpdateManager : Singleton<ScriptUpdateManager> {
		protected ScriptUpdateManager() { } // ensure only one instance

		List<UpdatableScript> updateScripts = new List<UpdatableScript>();
		List<UpdatableScript> lateUpdateScripts = new List<UpdatableScript>();
		List<UpdatableScript> fixedUpdateScripts = new List<UpdatableScript>();

		void Update() {
			for(int a = updateScripts.Count - 1; a >= 0; a--) {
				updateScripts[a].Update();
			}
		}

		void LateUpdate() {
			for(int a = updateScripts.Count - 1; a >= 0; a--) {
				lateUpdateScripts[a].LateUpdate();
			}
		}

		void FixedUpdate() {
			for(int a = updateScripts.Count - 1; a >= 0; a--) {
				fixedUpdateScripts[a].FixedUpdate();
			}
		}

		public void Register(UpdatableScript script, UpdateType updateType = UpdateType.UPDATE) {
			switch(updateType) {
				case UpdateType.UPDATE:
					updateScripts.Add(script);
					break;
				case UpdateType.LATE_UPDATE:
					lateUpdateScripts.Add(script);
					break;
				case UpdateType.FIXED_UPDATE:
					fixedUpdateScripts.Add(script);
					break;
			}
		}

		public void Withdraw(UpdatableScript script, UpdateType updateType = UpdateType.UPDATE) {
			switch(updateType) {
				case UpdateType.UPDATE:
					updateScripts.Remove(script);
					break;
				case UpdateType.LATE_UPDATE:
					lateUpdateScripts.Remove(script);
					break;
				case UpdateType.FIXED_UPDATE:
					fixedUpdateScripts.Remove(script);
					break;
			}
		}
	}
}