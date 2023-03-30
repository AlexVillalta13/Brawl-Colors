using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//using GameAnalyticsSDK;

public class Shop : MonoBehaviour {

	public int selectedSkin = 0;//persistance
	public int StarsForVideo;
	int numberOfButtonsSkins = 16;
	public static Shop instance;
	public GameObject store;
	public int currency;

	public int[] priceList;
	public bool[] unblockedSkins;
	public List<Transform> buttoneys;


	//Blue, Red, Green, Yellow, Dark Blue

	//public Color = new Color();
	public Color[] main; //6BA4E8FF, EC3B55FF, 74DB2DFF, E1CF3AFF, 3051CAFF
	public Color[] shadow; //496EBFFF, BD2E2EFF, 589C0BFF, CE990CFF, 587DCEFF
	public Color[] border; //ACC1EFFF, FBB3D5FF, C9F8BAFF, F8F6BDFF, ACBAEFFF

	public int[] shopStateIndexes; //Buy, Buy, Selected, Available, Sure

	public string[] shopItemStateText; //Buy, Buy, Selected, Available, Sure

	// Use this for initialization
	void Start () {
		instance = this;
		//int hi = #FBB3D5FF;
	}
	public void DrawButtonUpdate()
	{
		int counter = 0;
		foreach (Transform buttony in buttoneys)
		{
			Debug.Log ("shopStateIndexes [index]: " + shopStateIndexes [counter]);
			GameObject buyButton = buttoneys [counter].transform.Find ("buy_button").gameObject;
			buyButton.transform.Find ("main").GetComponent<Image> ().color = main [shopStateIndexes [counter]];
			buyButton.transform.Find ("shadow").GetComponent<Image> ().color = shadow [shopStateIndexes [counter]];
			buyButton.transform.Find ("border1").GetComponent<Image> ().color = border [shopStateIndexes [counter]];
			buyButton.transform.Find ("border2").GetComponent<Image> ().color = border [shopStateIndexes [counter]];
			buyButton.transform.Find ("buttonText").GetComponent<TextMeshProUGUI> ().text = shopItemStateText[shopStateIndexes[counter]];
			counter++;

			//GameManager.Instance.SavePersistance ();
		}
	}
	public void UpdateButtons(int index) //-1 = ALL
	{
		if (index == -1) {
			index = selectedSkin;
		}

		int county = 0;
		buttoneys = new List<Transform> ();
		foreach (Transform buttony in store.transform.Find("Content").transform) {
			if (buttony.name == "skin_board") {
				buttoneys.Add (buttony);
			}
		}
			
			if (shopStateIndexes [index] == 3) {
				shopStateIndexes [index] = 2;
				selectedSkin = index;
				for (int i = 0; i < shopStateIndexes.Length; i++) {
					if (i != index) {
						if (shopStateIndexes [i] == 0) {
						
						} else if (shopStateIndexes [i] == 1) {
							//shopStateIndexes [i] = 3;
						} else if (shopStateIndexes [i] == 4) {
							{
								shopStateIndexes [i] = 3;
							}
						} else {
							shopStateIndexes [i] = 3;
						}
					}
				} 

			} else if (shopStateIndexes [index] == 0) {
				shopStateIndexes [index] = 2;
				selectedSkin = index;
				for (int i = 0; i < shopStateIndexes.Length; i++) {
					if (i != index) {
						if (shopStateIndexes [i] == 2) {
							shopStateIndexes [i] = 3;

						} 
					}
				} 
			}

			foreach (Transform buttony in store.transform.Find("Content").transform) {
				if (buttony.name == "skin_board") {
					//buttoneys.Add (buttony);

					if (priceList [county] > currency) {
						if (shopStateIndexes [county] == 0) {
							shopStateIndexes [county] = 1;
						}
					} else {
						if (shopStateIndexes [county] == 1) {
							shopStateIndexes [county] = 0;
						}
					}
					county++;
				}
			}
		DrawButtonUpdate ();
			/*
		if (buttoneys [shopStateIndexes [index]]) {
		}*/
			/*
				Debug.Log ("ButtonHere");
				Debug.Log ("index : " + index + " counter: " + counter);d
					//Not enough money!!!
				} 
				else if (shopStateIndexes [counter] == 0) 
				{
					if (index == counter) {
						shopStateIndexes [counter] = 2;
						newSelected = counter;
					}
				}
				else if (shopStateIndexes[counter] == 3) 
				{
					if (counter == index) {// && newSelected != counter) 
						shopStateIndexes [counter] = 2;
					} 
					else 
					{
						shopStateIndexes [counter] = 3;
					}
				}

				

				counter++;

			}*/
		
	}


	public void PressButton(int index)
	{
		Debug.Log ("Pressed Button");
		Debug.Log ("Currency: " + currency);
			
		if (unblockedSkins [index] == false) {
			Debug.Log ("Skin was locked");
			if (currency >= priceList [index]) {
				Debug.Log ("Currency is more");
				currency -= priceList [index];
				//Shop.instance.currency -= priceList [index];
				unblockedSkins [index] = true;
				GameManager.Instance.selectedSkinIndex = index;
				GameManager.Instance.StoreCurrencyText.text = "" + Shop.instance.currency;

				GameManager.Instance.numberOfBoughtSkins++;
				//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, GameManager.Instance.numberOfBoughtSkins + " Skins Bought");
			} 
		}
		else 
		{
			Debug.Log ("Skin already available");
			GameManager.Instance.selectedSkinIndex = index;
		}
		GameManager.Instance.StoreCurrencyText.text = "" + Shop.instance.currency;
		//GameManager.Instance.SavePersistance ();
		Grid.instance.ResetGrid ();

		

		UpdateButtons (index);
	}
}
