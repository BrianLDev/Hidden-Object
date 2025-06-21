using UnityEngine;
using UnityEngine.InputSystem;
using Lean.Touch;

// TODO: Zoom in on mouse position

public class ScrollZoomCamera : MonoBehaviour {

	[SerializeField] private float scrollZoomFactor = 5f; // actual values are around .05, so multiply/divide by 100 when using
	private float scalar = 100f;
	private InputAction scrollAction;
	private Vector2 scrollDelta;
	private LeanPinchCamera leanPinchCamera;


	private void Start() {
		scrollAction = InputSystem.actions.FindAction("ScrollWheel");
		leanPinchCamera = ImageManager.Instance.leanPinchCamera;
	}

	private void Update() {
		scrollDelta = -scrollAction.ReadValue<Vector2>();

		if (scrollDelta.y != 0) {
			scrollDelta.y = scrollDelta.y > 0 ? scrollDelta.y / 2 : scrollDelta.y;  // slow zoom in since its faster than zoom out
			float zoomDelta = scrollDelta.y * (scrollZoomFactor / scalar);
			// Debug.Log($"ScrollDelta {scrollDelta}, zoomDelta {zoomDelta}, origZoom {ImageManager.Instance.targetZoom}, targetZoom {ImageManager.Instance.targetZoom + zoomDelta}");
			ImageManager.Instance.targetZoom += zoomDelta;
			ImageManager.Instance.targetZoom = Mathf.Clamp(ImageManager.Instance.targetZoom, leanPinchCamera.ClampMin, leanPinchCamera.ClampMax);
		}
	}

}