using UnityEngine;

namespace SoulFlare.Extensions {
	public static class LayerMaskExtensions{
		public static int NameToLayerMask(string layer) {
			return 1 << LayerMask.NameToLayer(layer);
		}
	}
}
