using UnityEngine;
using EcxUtilities;

public class ImageManager : Singleton<ImageManager> {
	[SerializeField] Camera cam;
	[SerializeField] SpriteRenderer spriteRenderer;

	public Vector3 ClampBounds(Vector3 targetPosition) {
		Vector3 camSize = new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize, 0);
		Vector3 minPos = GetImageMinPos() + new Vector3(camSize.x, camSize.y, 0);
		Vector3 maxPos = GetImageMaxPos() - new Vector3(camSize.x, camSize.y, 0);
		Vector3 newTargetPos;
		newTargetPos.x  = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
		newTargetPos.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);
		newTargetPos.z = targetPosition.z;
		// Debug.Log($"targetPos {targetPosition}, ImageMin {GetImageMinPos()}, ImageMax {GetImageMaxPos()}, minPos {minPos}, maxPos {maxPos}, newTargetPos {newTargetPos}");
		return newTargetPos;
	}

	public Vector3 GetImageMinPos() =>
		spriteRenderer.transform.position - spriteRenderer.bounds.size / 2;

	public Vector3 GetImageMaxPos() =>
		spriteRenderer.transform.position + spriteRenderer.bounds.size / 2;
}
