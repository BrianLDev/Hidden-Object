using UnityEngine;
using UnityEngine.EventSystems;

public class UIZoomImage : MonoBehaviour, IScrollHandler {

	private Vector3 initialScale;
	[SerializeField] private float zoomSpeed = 0.1f;
	[SerializeField] private float maxZoom = 10f;

	private void Awake() {
		initialScale = transform.localScale;
	}

	public void OnScroll(PointerEventData eventData)
	{
		var delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
		var desiredScale = transform.localScale + delta;

		// continue...
	}


}
