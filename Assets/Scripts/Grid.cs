using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Grid : MonoBehaviour {

	public static Grid instance;

	List<Block> blocksInGrid;
	int counter = 0;

	void Start()
	{
		instance = this; 

		blocksInGrid = new List<Block> ();
		for (int i = 0; i < 64; i++) {
			blocksInGrid.Add(transform.GetChild(i).GetComponent<Block>());
		}
		//ResetGrid ();
		//Debug.Log ("GameManager.Instance.selectedSkinIndex: " + GameManager.Instance.selectedSkinIndex);
	}

	public void ResetGrid()
	{
		foreach (Block blocky in blocksInGrid) {
			if (counter < 4) {
				blocky.colorIndex = 0;
				blocky.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites[(GameManager.Instance.selectedSkinIndex * 5) + 0]; 
			} 
			else 
			{
				blocky.colorIndex = 1;
				blocky.GetComponent<SpriteRenderer> ().sprite = GameManager.Instance.sprites[(GameManager.Instance.selectedSkinIndex * 5) + 1];
			}
			counter++;
			if (counter > 7) {
				counter = 0;
			}
		}
		counter = 0;
	}

	public void Appear()
	{
		transform.DOScale (new Vector3(1,1,1),0.3f);
	}
	public void Hide()
	{
		transform.DOScale (new Vector3(0,0,0),0.3f);
	}
}
