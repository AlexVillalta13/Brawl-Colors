using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHand : MonoBehaviour {

	public Vector2 targetPosition;
	public Vector2 startPiecePosition;
	public bool kill = false;


	void OnEnable()
	{
		startPiecePosition = transform.position;
		DoThingy ();
	}
	public void SetPositions(Vector2 start, Vector2 target)
	{
		targetPosition = target;
		transform.DOKill ();
		//DoThingy ();
		startPiecePosition = start;
	}
	public void DoThingy()
	{
		transform.DOScale(new Vector3(0.5f,0.5f,0.5f),0.7f).OnComplete (() => {
			if(kill)
			{
				kill = false;
				transform.DOKill();
			}
			transform.DOMove(targetPosition, 1, false).OnComplete (() => {
				if(kill)
				{
					kill = false;
					transform.DOKill();
				}
				transform.DOMove(targetPosition, 0.0f, false).OnComplete (() => {
					if(kill)
					{
						kill = false;
						transform.DOKill();
					}
					transform.DOScale(new Vector3(0.7f,0.7f,0.7f),0.7f).OnComplete (() => {
						if(kill)
						{
							kill = false;
							transform.DOKill();
						}
						transform.position = startPiecePosition;
						DoThingy();
					});
				});
			});
		});
	}
	public void RestartHand()
	{
		transform.DOKill ();
		transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
		transform.position = PieceGenerator.instance.piecesToRecover [0].GetComponent<Piece> ().startPosition;
		DoThingy ();
	}

}
