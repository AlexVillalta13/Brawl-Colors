using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchInput : MonoBehaviour
{
	public float yTouchSpace;
	public LayerMask touchInputMask;
	RaycastHit hit;

	bool realTouching = false;

	Piece recipient = null;

	public bool holdingObject = false;

	void Update ()
	{

		//Debug.Log ("" + Application.persistentDataPath);
		if (GameManager.Instance.gamePaused == false) {
			/*
			#if UNITY_EDITOR

			if (Input.GetMouseButton (0)) {
				//Debug.Log("Mousing");
				Vector2 position = Input.mousePosition; 
				position = (Vector2)Camera.main.ScreenToWorldPoint (position);
				//Debug.Log ("Position: " + position);

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 500.5f, touchInputMask)) {
					//if (hit.transform.tag == "pieceMoveable") {
					
					//Debug.Log ("Touched Mask");
					recipient = hit.transform.gameObject;

					if (Input.GetMouseButtonDown (0)) {
						holdingObject = true;

						recipient.GetComponent<Piece> ().BecomeSelected ();
					}
						
					if (Input.GetMouseButtonUp (0)) {
						holdingObject = false;
						//Debug.LogError ("MouseUP");
						//recipient.GetComponent<Piece> ().BecomeUnSelected ();
					}
					if (Input.GetMouseButton (0)) {
						holdingObject = true;
					}
				}
				GameManager.Instance.selectedPiece = recipient;
				if (holdingObject) {
					if (recipient != null) {
						Piece recipiece = recipient.GetComponent<Piece> ();
						int rows = recipiece.rows;
						int cols = recipiece.cols;

						recipient.transform.position = position + new Vector2(0, yTouchSpace);
					}
				}
			}
			#endif
			*/
			if (Input.touchCount > 0) {
				Touch touch = Input.touches [0];
				Vector3 position3D = touch.position;
				Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint (position3D);

				if (realTouching) {
					if (holdingObject) {
						//Debug.Log ("Should be moving");
						if (GameManager.Instance.selectedPiece != null) {
							GameManager.Instance.selectedPiece.transform.position = position + new Vector2 (0, yTouchSpace);
						}
					}
				}

				Ray ray = Camera.main.ScreenPointToRay (touch.position);

				//if (PieceGenerator.instance.isHiding == false) {
				if (GameManager.Instance.tutorialPlayed) {

					if (touch.phase == TouchPhase.Began) {
						if (Physics.Raycast (ray, out hit, 500.5f, touchInputMask)) {
							if(hit.transform.gameObject.GetComponent<Piece>().isHiding == false)
							{
								realTouching = true;
								Piece recipient = hit.transform.gameObject.GetComponent<Piece>();
								GameManager.Instance.selectedPiece = recipient;
								PieceGenerator.instance.lastSelectedPiece = recipient;
								recipient.BecomeSelected ();
								holdingObject = true;
								//Debug.Log ("OnTouchDown");
							}
						}
					} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						if (realTouching) {
							//Debug.Log ("OnTouchEnded");
							holdingObject = false;
							GameManager.Instance.selectedPiece.doDrop = true;
							GameManager.Instance.lastSelectedPosition = GameManager.Instance.selectedPiece.transform.position;
							GameManager.Instance.selectedPiece = null;

							realTouching = false;
						}

					} else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {

					} 

				} else { //INSIDE TUTORIAL!!!
					if (touch.phase == TouchPhase.Began) {
						if (Physics.Raycast (ray, out hit, 500.5f, touchInputMask)) {

							if (hit.transform.gameObject.GetComponent<Piece>() == PieceGenerator.instance.piecesToRecover [0]) {
								if (hit.transform.gameObject.GetComponent<Piece> ().isHiding == false) {
									realTouching = true;
									Piece recipient = hit.transform.gameObject.GetComponent<Piece>();
									PieceGenerator.instance.lastSelectedPiece = recipient;
									GameManager.Instance.selectedPiece = recipient;
									recipient.BecomeSelected ();
									GameManager.Instance.stateTutorial = StateOfTutorial.Movement;
									//PieceGenerator.instance.ShowHand ();
									PieceGenerator.instance.HideHand ();
									holdingObject = true;
									//Debug.Log ("OnTouchDown");
								}
							}
						}
					} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						if (realTouching) {
							//Debug.Log ("OnTouchEnded");
							holdingObject = false;
							GameManager.Instance.selectedPiece.doDrop = true;
							if (GameManager.Instance.tutorialCurrentPieceIndex > 2) {
								GameManager.Instance.stateTutorial = StateOfTutorial.Complete;
							} else {
								GameManager.Instance.stateTutorial = StateOfTutorial.Animation;
								//PieceGenerator.instance.ShowHand ();
								PieceGenerator.instance.HideHand ();
								PieceGenerator.instance.HandVisible ();
							}
							GameManager.Instance.lastSelectedPosition = GameManager.Instance.selectedPiece.transform.position;
							GameManager.Instance.selectedPiece = null;
							realTouching = false;
						}

					} else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
						//Debug.Log ("realTouching" + realTouching);
						//Debug.Log ("holdingObject" + holdingObject);

						if (realTouching) {
							if (holdingObject) {
								//Debug.Log ("Should be moving");
								if (GameManager.Instance.selectedPiece != null) {
									GameManager.Instance.selectedPiece.transform.position = position + new Vector2 (0, yTouchSpace);
								}
							}
						}
					} 
				}
			}
		}
	}
}
