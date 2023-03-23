using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {

	public static PieceManager instance; 

	public List<GameObject> pieces;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public void CheckGameOver()
	{
		bool piecesFit = false;

		//This should change!
		if (pieces.Count == 0) {
			piecesFit = true;
		}

		foreach (Piece piecey in PieceGenerator.instance.piecesToRecover){// pieces) {

			piecesFit = piecey.CheckIfGameOver();

			if (piecesFit) {
				break;
			}
		}
		if (piecesFit == false) {

			//GAMEOVER!!!
			MenuManager.instance.GameOver();
		}

	
	}
	public void AddPieceToManager(GameObject pieceToAdd)
	{
		pieces.Add(pieceToAdd);
	}
	public void ClearPieces()
	{
		pieces.Clear ();
	}
	public void RemovePieceFromManager(GameObject pieceToRemove)
	{
		pieces.Remove(pieceToRemove);
		CheckGameOver();
	}
}
