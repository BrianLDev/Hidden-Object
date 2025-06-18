using UnityEngine;
using UnityEngine.EventSystems;

// TODO: Keep image centered on mouse/touch position.  Make sure image stays within bounds of screen.
// TODO: Touch input zooming

public class UIZoomImage : MonoBehaviour, IScrollHandler {

	[SerializeField] private float zoomSpeed = 0.01f;
	[SerializeField] private float smoothingSpeed = 4f;
	[SerializeField] private float maxZoom = 5f;
	private Vector3 initialScale, targetScale;

	private void Awake() {
		initialScale = transform.localScale;
		targetScale = initialScale;
	}

	private void Update() {
		bool zoomingIn = targetScale.x > transform.localScale.x;
		float smoothSpeedInOut = zoomingIn ? smoothingSpeed / 2 : smoothingSpeed; // Zoom in is faster than zoom out, so adjust speed
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * smoothSpeedInOut);
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
