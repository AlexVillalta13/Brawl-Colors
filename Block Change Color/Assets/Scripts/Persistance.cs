using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DemiumGames.Saveable;
public class Persistance : MonoBehaviour {

	public int highscore = 0;
	public bool tutorialSeen = false;

	void Start()
	{
		if(!File.Exists(Application.persistentDataPath + "/playerInfo.json"))
		{
			
			//Debug.Log ("FileNotExisted");
			PlayerData data = new PlayerData();

		}
		else
		{
			//Debug.Log ("FileExisted");

			try {
			PlayerData data = JsonFormatterHelper.Load<PlayerData> (Application.persistentDataPath, "playerInfo.json", true); 
			GameManager.Instance.highscore = data.highscore;
				highscore = data.highscore;
				Shop.instance.currency = data.currency;
				Shop.instance.unblockedSkins = data.unblockedSkins;
				Shop.instance.selectedSkin = data.selectedSkin;
				Shop.instance.shopStateIndexes = data.shopStateIndexes;
				tutorialSeen = data.tutorialSeen;
				GameManager.Instance.selectedSkinIndex = data.selectedSkin;
				GameManager.Instance.tutorialPlayed = data.tutorialPlayed;
				GameManager.Instance.gamesEverPlayed = data.gamesEverPlayed;
				GameManager.Instance.videosEverWatched = data.videosEverWatched;
				GameManager.Instance.numberOfBoughtSkins = data.numberOfBoughtSkins;


			} catch (Exception e){
				GameManager.Instance.highscore = 0;
				Shop.instance.currency = 0;
			};

			//Debug.Log ("HIGHSCORE IS : " + data.highscore);
		}
		GameManager.Instance.RefreshHighScore(highscore);
		Grid.instance.ResetGrid ();
		//File.Delete(Application.persistentDataPath + "/playerInfo.dat");

		if (GameManager.Instance.tutorialPlayed == false) {
			MenuManager.instance.StartGame ();
		}
	}

	public void SaveData()
	{
		PlayerData data = new PlayerData();

		if (data.highscore < GameManager.Instance.highscore) {
			data.highscore = GameManager.Instance.highscore;
			highscore = GameManager.Instance.highscore;
		}
		data.currency = Shop.instance.currency;
		data.unblockedSkins = Shop.instance.unblockedSkins;
		data.tutorialSeen = tutorialSeen;
		data.selectedSkin = GameManager.Instance.selectedSkinIndex;
		data.tutorialPlayed = GameManager.Instance.tutorialPlayed;
		data.shopStateIndexes = Shop.instance.shopStateIndexes;
		data.selectedSkin = Shop.instance.selectedSkin;
		data.gamesEverPlayed = GameManager.Instance.gamesEverPlayed;
		data.videosEverWatched = GameManager.Instance.videosEverWatched;
		data.numberOfBoughtSkins = GameManager.Instance.numberOfBoughtSkins;

		JsonFormatterHelper.Save (data, Application.persistentDataPath, "playerInfo.json", true); 

		Debug.Log("Saved");
	}
}

[Serializable]
class PlayerData
{
	public int gamesEverPlayed = 0;
	public int videosEverWatched = 0;
	public int highscore = 0;
	public int selectedSkin = 0;
	public int numberOfBoughtSkins = 0;
	public int currency = 0;
	public bool tutorialSeen = false;
	public bool tutorialPlayed = false;

	//Should make another bool[] if more skins are added!!! or it will errase progress
	public bool[] unblockedSkins = new bool[] {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
	public int[] shopStateIndexes = new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}

