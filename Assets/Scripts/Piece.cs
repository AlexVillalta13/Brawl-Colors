using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour {

	//Temporary
	public bool doDrop = false;
	public bool isSelected = false;

	public bool isHiding = false;
	//Temporary

	public bool touchingGrid;
	public int colorIndex;
	public int startPosIndex;
	public Vector2 startPosition;

	public List <GameObject> children;
	public List <Vector2> childrenPositionsFromStart;
	public List <Vector2> gameOverCheckPositions;

	//For having old blocks errased after not touching
	public List <Block> blocksBeingTouched;
	public List <Block> blocksBeingTouchedNew;

	public List <Block> blocksWithTransparentSides;

	public int rows;
	public int cols;

	RaycastHit hit;
	public LayerMask hitLayerMask;
	public Vector2 yFixPosition;
	// Use this for initialization
	void OnEnable() {
		children = new List<GameObject> ();
		blocksBeingTouched = new List<Block> ();

		GetChildrenInPiece();
		PieceManager.instance.AddPieceToManager (this.gameObject);
		//CheckIfGameOver ();
	}
	public void BecomeSelected()
	{
		AudioManager.instance.PlaySound (0);
		isSelected = true;

		transform.DOKill ();
		transform.DOScale (new Vector3(1,1,1),0.15f).OnComplete (() => {
			transform.localScale = new Vector3(1,1,1);
			Debug.Log("LocalScale");
		});

		SpriteRenderer[] pieces = transform.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer rendy in pieces) {
			rendy.sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + colorIndex + 3];
		}

	}
	public void BecomeUnSelected() 
	{
		
		isSelected = false;

		transform.DOKill ();
		transform.DOScale (0.7f,0.005f);
		transform.localScale = new Vector3(0.7f,0.7f,1);

		yFixPosition = new Vector2(0, (0.2f * (4 - cols)));

		transform.DOMove (PieceGenerator.instance.startPositions[startPosIndex] + yFixPosition, 0.2f).OnComplete (() => {
			transform.DOShakeScale (0.5f,0.1f,15,90,true).OnComplete (() => {
				transform.localScale = new Vector3(0.7f,0.7f,1);
			});
			/*
			DOVirtual.DelayedCall(0.3f, ()=>{
				transform.DOLocalMoveX(2,2); 
			});
			*/
		});
	}
	// Update is called once per frame
	void Update () {

		if (GameManager.Instance.gamePaused == false) {

			CheckPositions ();
			
			if (doDrop) {
				//CheckIfGameOver ();
				doDrop = false;
				bool ok = CheckIfMoveAcceptable ();
				if (ok) {
					ok = false;
					AcceptPiece ();

					//Debug.LogError ("OK");
				} else {
					//Debug.LogError ("NOT OK");
					AudioManager.instance.PlaySound (2);
					BecomeUnSelected ();

				}
			}
		}
	}

	public bool CheckIfGameOver()
	{
		for (int i = 0; i < 8; i++) {
			for (int x = 0; x < 8; x++) {

				bool pieceFits = true;

				foreach (GameObject piecey in children) {
					Vector2 vecty = new Vector2 ((GameManager.Instance.firstBlockPos0coma0.x + piecey.transform.localPosition.x + 0) + ((rows - 1) * 0.25f) + 0.0f  ,GameManager.Instance.firstBlockPos0coma0.y + piecey.transform.localPosition.y + 0 - ((cols) * 0.5f) - 0.0f);
					//Vector2 vecty = new Vector2 ((GameManager.Instance.firstBlockPos0coma0.x + piecey.transform.position.x + transform.position.x) + ((rows - 1) * 0.25f) + 0.0f  ,GameManager.Instance.firstBlockPos0coma0.y + piecey.transform.position.y + transform.position.y - ((cols) * 0.5f) - 0.0f);
					gameOverCheckPositions.Add (vecty);
					//Debug.DrawRay(vecty, Vector3.forward, Color.green);
					//Debug.DrawRay(vecty + new Vector2(i/2, -x/2), Vector3.forward, Color.red);
					//Debug.DrawRay(vecty + new Vector2((float)3/2, (float) -3/2), Vector3.forward, Color.red);
					if (Physics.Raycast (new Vector3(vecty.x,vecty.y, 0) + new Vector3((float)i/2, (float)-x/2,0), transform.forward, out hit, hitLayerMask)) 
					{
						GameObject hitBlock = hit.collider.gameObject;
						Block hitBlockScript = hitBlock.GetComponent<Block> ();

						if (hitBlockScript.colorIndex == -1) {
							pieceFits = false;
						}
						else if (hitBlockScript.colorIndex != piecey.transform.parent.GetComponent<Piece> ().colorIndex) {

						} 
						else {
							pieceFits = false;
						}
					}
				}
				if (pieceFits) {
					//Debug.Log ("i: " + i + " x: " + x);
					return pieceFits;
				}
			}

		}
		return false;
	}

	void GetChildrenInPiece()
	{
		int childrenAmount = transform.childCount;
		for (int i = 0; i < childrenAmount; i++) {
			GameObject child = transform.GetChild (i).gameObject;
			children.Add (child);
			childrenPositionsFromStart.Add(child.transform.localPosition);
		}
	}
	public bool CheckIfMoveAcceptable()
	{
		bool correct = true;
		if (blocksBeingTouchedNew.Count > 0) {
			foreach (Block block in blocksBeingTouchedNew) {
				Block blocky = block;
				if (blocky.incorrectSquare || blocky.transparentSides) {
					correct = false;
					//this.transform.position = startPosition;
					return correct;
				}
			}
			return correct;
		}
		//this.transform.position = startPosition;
		//AudioManager.instance.PlaySound (2);
		return false;
	}
	void AcceptPiece()
	{
		AudioManager.instance.PlaySound (1);
		foreach(Block block in blocksBeingTouchedNew)
		{
			Block blocky = block;
			if(blocky.colorIndex == 0)
			{
				blocky.colorIndex = 1;
				blocky.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 1];
			}
			else
			{
				blocky.colorIndex = 0;
				blocky.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 0];
			}
		

		}

		PieceGenerator.instance.KillPiece (this, true);
		PieceManager.instance.RemovePieceFromManager (this.gameObject);
		//////

		if (GameManager.Instance.tutorialPlayed == false) {
			if (GameManager.Instance.tutorialCurrentPieceIndex < 2) {
				GameManager.Instance.stateTutorial = StateOfTutorial.Animation;
				GameManager.Instance.tutorialCurrentPieceIndex++;
			} else {
				GameManager.Instance.stateTutorial = StateOfTutorial.Complete;
				PieceGenerator.instance.HideHand ();

				GameManager.Instance.tutorialPlayed = true;
				//GameManager.Instance.persistance.SaveData ();
			}
		}
			
		GameManager.Instance.AddPoints (blocksBeingTouchedNew.Count);
		isSelected = false;
		touchingGrid = false;

		//PieceGenerator.instance.ShowHand ();
		//PieceGenerator.instance.startingGame_forStar = true;
	}


	void CheckPositions()
	{
		//Ray ray;

		blocksBeingTouched = blocksBeingTouchedNew.GetRange(0, blocksBeingTouchedNew.Count);
		blocksBeingTouchedNew.Clear ();

		touchingGrid = false;
		blocksWithTransparentSides.Clear ();
		foreach (GameObject child in children) {
			//ray = Camera.main.ScreenPointToRay (child.transform.position);
			if (Physics.Raycast (child.transform.position, transform.forward, out hit, hitLayerMask)) 
			{
				GameObject hitBlock = hit.collider.gameObject;
				Block hitBlockScript = hitBlock.GetComponent<Block> ();
				if (hitBlockScript.transparentSides == true) {
					if (blocksWithTransparentSides.Contains (hitBlockScript)) {

					} 
					else 
					{
						blocksWithTransparentSides.Add (hitBlockScript);
					}
				}
				else
				{

					if (hitBlockScript.colorIndex == child.transform.parent.GetComponent<Piece> ().colorIndex) {
						//Debug.Log ("SameColor!");
						hitBlock.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 4];
						hitBlockScript.incorrectSquare = true;
					}
					else
					{
						hitBlock.GetComponent<Block> ().incorrectSquare = false;
						if (hitBlockScript.colorIndex == 0) {
							hitBlock.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 3];
						} 
						else 
						{
							hitBlock.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 2];
						}
					}

					touchingGrid = true;
					blocksBeingTouchedNew.Add (hit.collider.gameObject.GetComponent<Block>());
				}
			}
		}
		if (touchingGrid) {
			foreach (Block block in blocksWithTransparentSides) {

				blocksBeingTouchedNew.Add (block);

			}
			//blocksWithTransparentSides.Clear ();
			foreach (Block block in blocksBeingTouchedNew) {
				if (block.transparentSides) {
					block.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + 4];
				} 
				else
				{

					//block.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.persistance.selectedSkinIndex * 5) + colorIndex];
				}
			}
			foreach (GameObject child in children) {
				child.GetComponent<SpriteRenderer> ().sprite = null;
			}
		}
		else 
		{
			foreach (GameObject child in children) {
				if (isSelected == true) {
					child.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + colorIndex + 2];
				} 
				else 
				{
					child.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites [(GameManager.Instance.selectedSkinIndex * 5) + colorIndex];
				}
			}

			//Nothing
		}

		foreach (Block block in blocksBeingTouched) 
		{
			bool stillTouched = false;

			foreach (Block piece in blocksBeingTouchedNew) 
			{
				if (block == piece)
				{
					stillTouched = true;
				}
			}
			if (!stillTouched) {
				if (block.transparentSides) 
				{
					block.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.spriteZeroAlpha; //sprites [(GameManager.Instance.selectedSkinIndex * 5) + 2];
				} 
				else 
				{
					block.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.sprites[(GameManager.Instance.selectedSkinIndex * 5) + block.colorIndex];
				}
			}
		}
	}
}
