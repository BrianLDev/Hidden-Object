using UnityEngine;
using EcxUtilities;
using Lean.Touch;

// TODO: Consider merging/centralizing 3 zoom methods (1. LeanPinchCamera, 2. ImageManager, 3. ScrollZoomCamera)

public class ImageManager : Singleton<ImageManager> {
	[SerializeField] private Camera cam;
	[SerializeField] private SpriteRenderer spriteRenderer; // for the hidden object image
	[SerializeField] public LeanPinchCamera leanPinchCamera;
	[SerializeField] private float zoomSpeed = 3f;
	[SerializeField] private float zoomInFraction = 0.05f;
	[HideInInspector] public float targetZoom;
	private const float epsilon = 0.0001f;  // TODO: Move this to a global constants class at some point


	private void Start() {
		if (cam == null)
			cam = Camera.main;
		if (spriteRenderer == null)
			Debug.LogError("Error: Image manager needs a reference to the Hidden Object's sprite renderer");
		if (leanPinchCamera == null)
			leanPinchCamera = FindAnyObjectByType<LeanPinchCamera>();
		targetZoom = leanPinchCamera.Zoom;
	}

	private void Update() {
		if (Mathf.Abs(leanPinchCamera.Zoom - targetZoom) > epsilon)
			leanPinchCamera.Zoom = Mathf.Lerp(leanPinchCamera.Zoom, targetZoom, Time.deltaTime * zoomSpeed);
	}

	public Vector3 ClampBounds(Vector3 targetPosition) {
		Vector3 camSize = new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize, 0);
		Vector3 minPos = GetImageMinPos() + new Vector3(camSize.x, camSize.y, 0);
		Vector3 maxPos = GetImageMaxPos() - new Vector3(camSize.x, camSize.y, 0);
		if (minPos.x > maxPos.x || minPos.y > maxPos.y) {
			// Debug.Log($"Min image pos {minPos} > max image pos {maxPos}.  Zooming in");
			ZoomIn(zoomInFraction);
			return targetPosition;
		}
		Vector3 newTargetPos;
		newTargetPos.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
		newTargetPos.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);
		newTargetPos.z = targetPosition.z;
		// Debug.Log($"targetPos {targetPosition}, ImageMin {GetImageMinPos()}, ImageMax {GetImageMaxPos()}, minPos {minPos}, maxPos {maxPos}, newTargetPos {newTargetPos}");
		return newTargetPos;
	}

	public Vector3 GetImageMinPos() =>
		spriteRenderer.transform.position - spriteRenderer.bounds.size / 2;

	public Vector3 GetImageMaxPos() =>
		spriteRenderer.transform.position + spriteRenderer.bounds.size / 2;

	public void ZoomIn(float fraction) =>
		targetZoom = leanPinchCamera.Zoom * (1 - fraction);

}
