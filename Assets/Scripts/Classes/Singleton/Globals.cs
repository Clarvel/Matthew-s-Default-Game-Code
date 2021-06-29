namespace SoulFlare.Singleton {
	/// <summary>
	/// Globals Singleton
	/// Usage: Globals.Instance.VARIABLE
	/// </summary>
	public class Globals : Singleton<Globals> {
		protected Globals() { } // garantee this will always be a singleton
		// PUT VARIABLES AND MONOBEHAVIOUR CODE HERE

		void Awake() {
			
		}
	}
}