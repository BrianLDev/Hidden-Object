using UnityEngine;
using Lean.Touch;
using UnityEngine.InputSystem;

public class ScrollZoomCamera : MonoBehaviour {
	[SerializeField] LeanPinchCamera leanPinchCamera;
	[SerializeField] private float zoomSpeed = 5f;	// actual values are around .005, so multiply/divide by 1000 to adjust
	[SerializeField] private float smoothingSpeed = 2f;
	private InputAction scrollAction;
	private Vector2 scrollDelta;
	private float targetZoom;

	private void Start() {
		if (leanPinchCamera == null)
			leanPinchCamera = FindAnyObjectByType<LeanPinchCamera>();
		scrollAction = InputSystem.actions.FindAction("ScrollWheel");
		targetZoom = leanPinchCamera.Zoom;
	}

	private void Update() {
		scrollDelta = -scrollAction.ReadValue<Vector2>();
		if (scrollDelta.y != 0) {
			scrollDelta.y = scrollDelta.y > 0 ? scrollDelta.y / 2 : scrollDelta.y;	// slow zoom in since its faster than zoom out
			targetZoom += scrollDelta.y * (1 + zoomSpeed / 1000);
			targetZoom = Mathf.Clamp(targetZoom, leanPinchCamera.ClampMin, leanPinchCamera.ClampMax);
		}
		leanPinchCamera.Zoom = Mathf.Lerp(leanPinchCamera.Zoom, targetZoom, Time.deltaTime * smoothingSpeed);
	}

}