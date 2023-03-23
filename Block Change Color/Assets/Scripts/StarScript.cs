using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarScript : MonoBehaviour {

	public PathType pathType = PathType.CatmullRom;
	public Vector3[] waypoints;

	public RectTransform destination;

	// Use this for initialization
	void Start () {
		StartStar ();
	}
	public void StartStar ()
	{
		//Vector3 actualPosition = Camera.main.WorldToViewportPoint(destination.position);

		Vector2 lastPositionPlaced = GameManager.Instance.lastSelectedPosition;
		waypoints = new Vector3[] {
			new Vector3(lastPositionPlaced.x + 0.2f, lastPositionPlaced.x + 0.2f, 0),
			new Vector3(lastPositionPlaced.x + 0.3f, lastPositionPlaced.x + 0.4f, 0),
			destination.anchoredPosition,
		};
		// Create a path tween using the given pathType, Linear or CatmullRom (curved).
		// Use SetOptions to close the path
		// and SetLookAt to make the target orient to the path itself
		transform.position = lastPositionPlaced;
		Tween t = transform.DOPath(waypoints, 10, pathType)
			.SetOptions(false);
		// Then set the ease to Linear and use infinite loops
		t.SetEase(Ease.Linear).SetLoops(1);
	}
}
