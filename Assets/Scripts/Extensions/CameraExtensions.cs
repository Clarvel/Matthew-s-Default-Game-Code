using UnityEngine;

namespace SoulFlare.Extensions {
	public static class CameraExtensions {
		public static void CullingMaskShow(this Camera cam, int layerMask) {
			cam.cullingMask |= layerMask;
		}
		public static void CullingMaskShow(this Camera cam, string layer) {
			cam.CullingMaskShow(LayerMaskExtensions.NameToLayerMask(layer));
		}

		public static void CullingMaskHide(this Camera cam, int layerMask) {
			cam.cullingMask &= ~layerMask;
		}
		public static void CullingMaskHide(this Camera cam, string layer) {
			cam.CullingMaskHide(LayerMaskExtensions.NameToLayerMask(layer));
		}

		public static void CullingMaskToggle(this Camera cam, int layerMask) {
			cam.cullingMask ^= layerMask;
		}
		public static void CullingMaskToggle(this Camera cam, string layer) {
			cam.CullingMaskToggle(LayerMaskExtensions.NameToLayerMask(layer));
		}

		public static bool CullingMaskIncludes(this Camera cam, int layerMask) {
			return (cam.cullingMask & layerMask) > 0;
		}
		public static bool CullingMaskIncludes(this Camera cam, string layer) {
			return cam.CullingMaskIncludes(LayerMaskExtensions.NameToLayerMask(layer));
		}
	}
}
