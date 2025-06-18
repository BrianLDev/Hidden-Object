using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, IScrollHandler {

	[SerializeField] private Camera cam;
	[SerializeField] private float zoomSpeed = 0.01f;
	[SerializeField] private float smoothingSpeed = 4f;
	[SerializeField] private float maxZoom = 5f;
	private Vector3 initialScale, targetScale;
	private Vector3 dragStart, dragVec;


	private void Update() {

		if (Mouse.current.leftButton.isPressed) {
			Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

			if (Mouse.current.leftButton.wasPressedThisFrame)
				dragStart = mouseWorldPos;
			dragVec = dragStart - mouseWorldPos;
			PanCamera();
		}
	}

	private void PanCamera() {
		cam.transform.position += dragVec;
	}

	private void ZoomCamera() {
		
	}

	public void OnScroll(PointerEventData eventData) {
		Vector3 delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
		targetScale = transform.localScale + delta;

		targetScale = ClampTargetScale(targetScale);
	}

	private Vector3 ClampTargetScale(Vector3 scale) {
		scale = Vector3.Max(initialScale, scale);
		scale = Vector3.Min(initialScale * maxZoom, scale);
		return scale;
	}
}
