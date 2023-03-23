using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameAnalyticsSDK;

public class PieceGenerator : MonoBehaviour {

	public GameObject starObject;
	public Piece lastSelectedPiece;
	public TutorialHand handTutorialObject;

	public bool startingGame_forStar = true;
	public float tweenSpeed;

	public int randomPossibleChoices;
	public bool goingBackToMenu = false;
	public static PieceGenerator instance;

	public List<Piece> spawnedPieces;

	public Vector2[] startPositions;
	public Vector2[] startOutOfScreenPositions;

	//PieceManager.instance.pieces.Count public int piecesLeftCounter = 4;

	public List<Piece> piecesToPlace;
	//public List<Vector2> piecesToPlaceInitualPos;
	public List<Piece> piecesToRecover;

	public bool isHiding = true;

	bool lastColorWasPrimary;

	void Awake()
	{
		instance = this;
		spawnedPieces = new List<Piece> ();
	}

	public void HandVisible()
	{
		handTutorialObject.GetComponent<TutorialHand>().RestartHand();
		handTutorialObject.gameObject.SetActive (true);
		//ShowHand (piecesToRecover.Count);
		//handTutorialObject.transform.DOKill ();
	}

	public void ShowHand(int piecesLeftInt)
	{
		Debug.Log ("piecesToRecover.Count: " + piecesToRecover.Count);
		if (GameManager.Instance.stateTutorial == StateOfTutorial.Complete) {
		} else {
			Debug.Log ("even: " + GameManager.Instance.tutorialCurrentPieceIndex % 2);

			if (piecesToRecover.Count == 4) {	
				handTutorialObject.SetPositions (piecesToRecover [0].transform.position, new Vector2 (1, 0.7f));

				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "Step3 Tutorial First Piece Placed");
			} else if (piecesToRecover.Count == 3) {
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step3 Tutorial First Piece Placed");
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "Step4 Tutorial Second Piece Placed");
				handTutorialObject.SetPositions (piecesToRecover [0].transform.position, new Vector2 (-1, -0.4f));
			} else if (piecesToRecover.Count == 2) {
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step4 Tutorial Second Piece Placed");
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "Step5 Tutorial Third Piece Placed");

				handTutorialObject.SetPositions (piecesToRecover [0].transform.position, new Vector2 (1, -0.9f));
			} else if (piecesToRecover.Count == 1) {
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step5 Tutorial Third Piece Placed");
				GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "Step6 First Game Finished");
			}

			handTutorialObject.GetComponent<TutorialHand>().RestartHand();
			/*
		if (piecesLeftInt % 2 == 0){//GameManager.Instance.tutorialCurrentPieceIndex % 2 == 0) {
				handTutorialObject.SetPositions (piecesToRecover [0].transform.position, new Vector2(1,0.7f));
		} 
		else 
		{
				
			handTutorialObject.SetPositions (piecesToRecover [0].transform.position, new Vector2(-1,-0.4f));
		}
		//SetPositions (piecesToRecover [GameManager.Instance.tutorialCurrentPieceIndex].transform.position, );
		}
		*/
		}
	}
	public void HideHand ()
	{
		handTutorialObject.gameObject.SetActive (false);
		if (GameManager.Instance.stateTutorial == StateOfTutorial.Complete) {
			
		}
	}
	public void TakePiecesAway()
	{	
		goingBackToMenu = true;
		
		foreach (Piece piecey in piecesToRecover) {

			piecey.isHiding = true;
			piecey.transform.DOLocalMove (startOutOfScreenPositions[piecey.startPosIndex],1,false).OnComplete (() => {
				KillPiece(piecey, true);
				piecey.isSelected = false;
				piecey.touchingGrid = false;
				piecey.transform.localScale = new Vector3(0.7f,0.7f,1);
				GameManager.Instance.gamePaused = true;
				piecey.gameObject.SetActive(false);
			});
		}
		PieceManager.instance.ClearPieces ();
		//piecesLeftCounter = 4;
	}

	public void HidePieces(bool onlyHiding)
	{
		if (onlyHiding) {
			GameManager.Instance.gamePaused = false;
		}
		isHiding = true;
		foreach (Piece piecey in piecesToRecover) {
			piecey.isHiding = true;
			piecey.transform.DOLocalMove (startOutOfScreenPositions[piecey.startPosIndex],1,false).OnComplete (() => {
				KillPiece(piecey, true);
				piecey.isSelected = false;
				piecey.touchingGrid = false;
				piecey.transform.localScale = new Vector3(0.7f,0.7f,1);
				GameManager.Instance.gamePaused = true;
				piecey.gameObject.SetActive(false);
			});
		}
		PieceManager.instance.ClearPieces ();
		GameManager.Instance.ResetScore ();
		//StartSpawnGame();
	}
	public void AddStar()
	{
		GameManager.Instance.AddCurrency (1);
		starObject.transform.position = GameManager.Instance.lastSelectedPosition + new Vector2(0,0.75f);
		starObject.SetActive (true);
		/*starObject.transform.DOLocalMoveY (2, 0.7f, false).OnComplete (() => {
			starObject.transform.DOLocalMoveY (2, 0.3f, false).OnComplete(OnStarAnimation);
		});*/

			

		//Vector2() = new Vector2[]
		//starObject.transform.DOPath()
	}
	public void OnStarAnimation()
	{
		//Shop.instance.currency++;
		starObject.SetActive (false);
	}

	public void StartSpawnGame()
	{
		//PieceGenerator.instance.piecesLeftCounter = 4;
		if (startingGame_forStar == false) {
			//AddStar ();
			Debug.Log ("AddStar!!");
			startingGame_forStar = true;
		} 
		else {
			
		}
		Piece gamey;
		Vector2[] yFixPosition = new Vector2[4];
		//SpawnPiece(0, false);
		for (int i = 0; i < startPositions.Length; i++) 
		{
			if (i % 2 == 0) {
				gamey = SpawnPiece(i, true);
			} 
			else 
			{
				gamey = SpawnPiece(i, false);
			}
			gamey.startPosIndex = i;

			yFixPosition[i] = new Vector2(0, (0.2f * (4 - gamey.cols)));

			spawnedPieces.Add (gamey);
			//gamey.transform.localScale = new Vector3(0.7f,0.7f,1);
		}
		isHiding = true;
		spawnedPieces[0].transform.DOMove (startPositions[0] + yFixPosition[0], tweenSpeed).OnComplete (() => {
			spawnedPieces[0].isHiding = false;
			spawnedPieces[1].transform.DOMove (startPositions[1] + yFixPosition[1], tweenSpeed).OnComplete (() => {
				spawnedPieces[1].isHiding = false;
				spawnedPieces[2].transform.DOMove (startPositions[2] + yFixPosition[2], tweenSpeed).OnComplete (() => {
					spawnedPieces[2].isHiding = false;
					spawnedPieces[3].transform.DOMove (startPositions[3] + yFixPosition[3], tweenSpeed).OnComplete (() => {
						spawnedPieces[3].isHiding = false;
						spawnedPieces.Clear ();
						GameManager.Instance.gamePaused = false;
						PieceGenerator.instance.goingBackToMenu = false;
						PieceGenerator.instance.startingGame_forStar = false;
						isHiding = false;
						if(GameManager.Instance.tutorialPlayed == false)
						{
							if(GameManager.Instance.stateTutorial != StateOfTutorial.Complete)
							{
								ShowHand (0);
								HandVisible();
							}
							else
							{
								HideHand();
							}
						}
						//PieceGenerator.instance.piecesLeftCounter = 4;
					});
				});
			});
		});

		PieceManager.instance.CheckGameOver ();
	}

	public Piece SpawnPiece(int indexToSpawnAt, bool primaryColor)
	{
		Piece piece;
		int random = Random.Range (0, randomPossibleChoices);
		piece = piecesToPlace [random];

		//piece.transform.position = (Vector2) startPositions [indexToSpawnAt];
		piecesToPlace.Remove (piece);
		piecesToRecover.Add (piece);

		Piece piecey = piece;

		Vector2 yFixPosition = new Vector2(0, (0.2f * (4 - piecey.cols)));
		piecey.startPosition = (Vector2)startPositions [indexToSpawnAt] + yFixPosition;
		piece.transform.position = (Vector2)startOutOfScreenPositions [indexToSpawnAt] + yFixPosition;
		//transform.localScale = new Vector3(0.7f,0.7f,1);
		if (!primaryColor) {
			piecey.colorIndex = 1;
			foreach (Transform piecy in piece.transform) {
				piecy.gameObject.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 1];
			}
		} 
		else 
		{
			piecey.colorIndex = 0;
			foreach (Transform piecy in piece.transform) {
				piecy.gameObject.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 0];
			}
		}
		piece.gameObject.SetActive (true);

		return piece;
	}

	public void KillPiece(Piece piece, bool lastColorPrimary)
	{
		//piecesLeftCounter--;
		Debug.Log ("Killed Piece");
		piecesToPlace.Add (piece);
		piecesToRecover.Remove (piece);
		piece.isSelected = false;
		piece.touchingGrid = false;
		piece.transform.localScale = new Vector3 (0.7f, 0.7f, 1);
		piece.transform.gameObject.SetActive (false);
		piece.transform.position = new Vector3 (0, -10, 0);

		Debug.Log ("PieceManager.instance.pieces.Count : " + PieceManager.instance.pieces.Count  + "\t" + "goingBackToMenu: " + goingBackToMenu + "\t" + "GameManager.Instance.gamePaused" + GameManager.Instance.gamePaused);
		if (PieceManager.instance.pieces.Count  <= 1 && goingBackToMenu == false) {


			AddStar ();
			StartSpawnGame ();



		} else if(PieceManager.instance.pieces.Count <= 1 && GameManager.Instance.gamePaused == false) {
			StartSpawnGame ();
		}
		if (GameManager.Instance.stateTutorial != StateOfTutorial.Complete) {
			ShowHand (piecesToRecover.Count);
		}
		//goingBackToMenu = false;
	}
}
