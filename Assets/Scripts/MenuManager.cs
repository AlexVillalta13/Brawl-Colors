using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using GameAnalyticsSDK;
using UnityEngine.UI;
using DemiumGames.AdMobManager;
using System;

public class MenuManager : MonoBehaviour {

	public static MenuManager instance; 

	public bool gameInterupted = false;
	public DateTime startGameTime;
	public DateTime endGameTime;

	public int sessionGold = 0;
	public int videosWatchedPerSession = 0;

	bool musicEffectsON = true;
	public GameObject menuTitleText;
	public Canvas canvas;
	public float timeLoadingVideo;

	GameObject panelTitle;
	GameObject panelTitleIcons;
	GameObject panelMainMenu;
	GameObject panelHomeButton;
	GameObject panelSettingButton;
	GameObject panelPaused;
	GameObject panelGameOver;
	GameObject panelStore;
	GameObject panelNoAds;
	GameObject panelSettings;
	GameObject panelPausedButton;

	public GameObject gameOverTextCurrency;
	public GameObject gameOverTextHighscore;

	public GameObject gameOverRewardButton;
	public GameObject storeRewardButton;

	public int homeButtonOption = 0;

	public Button buy1;
	public Button buy2;
	public Button buy3;
	public Button buy4;
	public Button buy5;
	public Button buy6;
	public Button buy7;
	public Button buy8;
	public Button buy9;
	public Button buy10;

	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Text text5;
	public Text text6;
	public Text text7;
	public Text text8;
	public Text text9;
	public Text text10;

	//GameObject panelLose;
	//GameObject panelStore;

	bool bannerLoaded = false;
	bool videoLoaded = false;
	//Ads
	bool interAdLoaded = false;

	int gamesCounter = 0;

	//Vector2 shopPositionOutOfScreen;

	Button button;

	public GameObject grid;

	void Start()
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step1 Splash to Main Screen");

		instance = this;
		panelStore = canvas.transform.Find ("PanelStore").gameObject;
		panelTitle = canvas.transform.Find ("PanelTitle").gameObject;
		panelTitleIcons = canvas.transform.Find("PanelTitleIcons").gameObject;
		panelGameOver = canvas.transform.Find ("PanelGameOver").gameObject;
		panelPaused = canvas.transform.Find ("PanelPaused").gameObject;
		panelMainMenu = canvas.transform.Find ("PanelMainMenu").gameObject;
		panelSettingButton = canvas.transform.Find ("PanelSettingButton").gameObject;
		panelPausedButton = canvas.transform.Find ("PanelPauseButton").gameObject;
		//panelNoAds = canvas.transform.Find ("PanelNoAds").gameObject;
		panelSettings = canvas.transform.Find ("PanelSettings").gameObject;
		panelHomeButton = canvas.transform.Find ("PanelHomeButton").gameObject;

		button = panelPaused.transform.Find("replay_square_button").Find("main").GetComponent<Button>();

		gameOverRewardButton = panelGameOver.transform.Find ("RewardedVideoButton").gameObject;
		storeRewardButton = panelStore.transform.Find ("Video_buttonSmall").gameObject;

		/*
		 * 
		 * VIDEO REWARD
		 * 
		 * 
		 * 
		 */

		//AdMobManager.Instance.SetOnRewardLoaded (() => {
		//	videoLoaded = true;
		//	gameOverRewardButton.SetActive(true);
		//	storeRewardButton.SetActive(true);
		//	//GameAnalytics.NewDesignEvent ("Video Loaded",1);
		//});
		//AdMobManager.Instance.SetOnRewardFailedToLoad(() => {
		//	videoLoaded = false;
		//	StartCoroutine(LoadingVideo());
		//	if(Application.internetReachability != NetworkReachability.NotReachable)
		//	{
		//		//GameAnalytics.NewDesignEvent ("Video Failed",1);
		//	}
		//});
		//AdMobManager.Instance.LoadVideo ();

		//AdMobManager.Instance.SetOnBannerFailedToLoad (() => {
		//	if(Application.internetReachability != NetworkReachability.NotReachable)
		//	{
		//		//GameAnalytics.NewDesignEvent ("Banner Failed",1);
		//	}
		//});
		//AdMobManager.Instance.SetOnBannerClicked(() => {
		//	//GameAnalytics.NewDesignEvent ("Banner Clicked",1);
		//});

		//AdMobManager.Instance.SetOnInterFailedToLoad (() => {
		//	if(Application.internetReachability != NetworkReachability.NotReachable)
		//	{
		//		//GameAnalytics.NewDesignEvent ("Inter Failed",1);
		//	}
		//});
		//AdMobManager.Instance.SetOnInterClicked(() => {
		//	//GameAnalytics.NewDesignEvent ("Inter Clicked",1);
		//});
		//AdMobManager.Instance.SetOnInterReturn (() => {
		//	//GameAnalytics.NewDesignEvent ("Inter Return",1);
		//});

		LoadRewardedVideo ();
	}

	public void StartGame()
	{
		startGameTime = DateTime.UtcNow;
		gameInterupted = false;

		
		//if (!bannerLoaded) {
		//	AdMobManager.Instance.LoadBanner (GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Top);
		//	bannerLoaded = true;
		//}
		//if (!videoLoaded ) {
		//	AdMobManager.Instance.LoadVideo ();
		//}



		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step2 Play Pressed");
		PieceGenerator.instance.goingBackToMenu = false;
		//AdMobManager.Instance.LoadBanner (GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Top);
		gamesCounter++;
		//if (bannerLoaded) {
		//	AdMobManager.Instance.ShowBanner(); 
		//}
		//AdMobManager.Instance.LoadInter ();

		//AdMobManager.Instance.SetOnInterLoaded (() => {

		//	//GameAnalytics.NewDesignEvent("Loaded Inter");
		//	//Debug.Log("CARGUË");
		//	interAdLoaded = true;
		//	//AdMobManager.Instance.ShowInter(); 
		//});



		panelMainMenu.transform.DOKill ();
		panelSettingButton.transform.DOKill ();
		panelPausedButton.transform.DOKill ();

		TweenObjects(panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().startPosition, true, false);
		TweenObjects(panelSettingButton, panelSettingButton.GetComponent<PanelScript> ().startPosition, true, false);
		TweenObjects(panelPausedButton, panelPausedButton.GetComponent<PanelScript> ().screenPosition, false, false);

		Grid.instance.ResetGrid ();
		//menuTitleText.transform.DOLocalMoveY (transform.position.y + 878.29f, 0.3f, false);
		menuTitleText.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f).OnComplete(() =>
		{
			grid.GetComponent<Grid>().Appear();
		});
		//GameAnalyticsSDK.GameAnalytics.NewDesignEvent ("StartGame", 1);
		GameManager.Instance.gamePaused = false;
		
		panelTitleIcons.SetActive (true);

		PieceGenerator.instance.startingGame_forStar = true;
		PieceGenerator.instance.StartSpawnGame();
	}

	private void RewardSuccess()
	{
		StartCoroutine(LoadingVideo());
		//LoadRewardedVideo ();
		videosWatchedPerSession++;
		Debug.Log ("HE VISTO EL VIDEO ENTERO"); 
		GameManager.Instance.RefreshHighScore (GameManager.Instance.highscore);
		gameOverRewardButton.SetActive (false);
		storeRewardButton.SetActive (false);
		
		Shop.instance.currency += Shop.instance.StarsForVideo;
		GameManager.Instance.videosEverWatched++;
		GameManager.Instance.RefreshHighScore (GameManager.Instance.highscore);
		GameManager.Instance.SavePersistance ();

		if (GameManager.Instance.videosEverWatched >= 50) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "50 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 40) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "40 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 30) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "30 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 20) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "20 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 15) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "15 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 10) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "10 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 5) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "5 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 4) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "4 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 3) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "3 Videos Watched");
		} else if (GameManager.Instance.videosEverWatched >= 2) {
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "2 Videos Watched");
		} 
		else 
		{
			//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "1 Video Watched");

		}
	}
	
	

	private void RewardFailed(){
		//LoadRewardedVideo ();
		StartCoroutine(LoadingVideo());
		Debug.Log ("NO HE VISTO EL VIDEO ENTERO"); 
		//GameAnalytics.NewDesignEvent ("Video Exited",1);
		gameOverRewardButton.SetActive (false);
		storeRewardButton.SetActive (false);
		videoLoaded = false;
	//AdMobManager.Instance.VI
		
	}
	public void ShowVideo()
	{
		if(videoLoaded)
		{		
			videoLoaded = false;
			//AdMobManager.Instance.ShowVideoAd(RewardFailed, RewardSuccess);
		}
	}
	public void RestartGame(int index) //0 panelLose, 1 panelPause
	{
		startGameTime = DateTime.UtcNow;
		gameInterupted = false;

		if (videoLoaded == false) {
			//AdMobManager.Instance.LoadVideo ();
		}
		//PieceGenerator.instance.pieces = 4;
		GameManager.Instance.gamePaused = false;
		//PieceGenerator.instance.startingGame_forStar = fa;

		//if (bannerLoaded) {
		//	AdMobManager.Instance.ShowBanner(); 
		//}
		//AdMobManager.Instance.DestroyInter();
		gamesCounter++;
		//GameAnalyticsSDK.GameAnalytics.NewDesignEvent ("StartGame", 1);

		button.interactable = true;
		Grid.instance.ResetGrid ();
		PieceGenerator.instance.HidePieces (false);
		PieceGenerator.instance.StartSpawnGame ();
		
		panelGameOver.transform.DOKill ();
		panelTitle.transform.DOKill ();
		panelTitleIcons.transform.DOKill ();

		if (index == 0) {
			TweenObjects (panelGameOver, panelGameOver.GetComponent<PanelScript> ().startPosition, true,true);
			TweenObjects (panelTitle, panelTitle.GetComponent<PanelScript> ().screenPosition, false,false);
			TweenObjects (panelTitleIcons, panelTitleIcons.GetComponent<PanelScript> ().screenPosition, false,false);
		}
		else
		{
			TweenObjects (panelPaused, panelPaused.GetComponent<PanelScript> ().startPosition, true,true);
		}
		GameManager.Instance.interLoaded = false;
		TweenObjects(panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true,false);
		TweenObjects(panelPausedButton, panelPausedButton.GetComponent<PanelScript> ().screenPosition, false,false);
		
		//PieceGenerator.instance.startingGame_forStar = false;
		//PieceGenerator.instance.goingBackToMenu = TR;
	}

	public void ShowSettings()
	{
		Grid.instance.Hide ();
		homeButtonOption = 2;

		panelSettings.transform.DOKill ();
		panelMainMenu.transform.DOKill ();
		panelHomeButton.transform.DOKill ();
		panelSettingButton.transform.DOKill ();
		panelTitle.transform.DOKill ();
		panelTitleIcons.transform.DOKill ();

		TweenObjects (panelSettings, panelSettings.GetComponent<PanelScript>().screenPosition, false, true);
		TweenObjects (panelMainMenu, panelMainMenu.GetComponent<PanelScript>().startPosition, true, false);
		TweenObjects(panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().screenPosition, false ,false);
		TweenObjects(panelSettingButton, panelSettingButton.GetComponent<PanelScript> ().startPosition, true ,false);
		TweenObjects(panelTitle, panelTitle.GetComponent<PanelScript> ().startPosition, true ,false);
		TweenObjects(panelTitleIcons, panelTitleIcons.GetComponent<PanelScript> ().startPosition, true ,false);
	}
	public void ShowShop()
	{
		Shop.instance.DrawButtonUpdate ();
		GameManager.Instance.StoreCurrencyText.text = "" + Shop.instance.currency;
		Grid.instance.Hide ();
		homeButtonOption = 1;
		panelStore.transform.DOKill ();
		panelMainMenu.transform.DOKill ();
		panelHomeButton.transform.DOKill ();
		panelSettingButton.transform.DOKill ();
		panelTitle.transform.DOKill ();
		panelTitleIcons.transform.DOKill ();
		panelGameOver.transform.DOKill ();

		TweenObjects(panelStore, panelStore.GetComponent<PanelScript> ().screenPosition, false,true);
		//panelMainMenu.transform.position = panelMainMenu.GetComponent<PanelScript> ().startPosition;
		panelMainMenu.SetActive (false);
		TweenObjects(panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().startPosition, false,true);
		TweenObjects(panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().screenPosition, false ,false);
		TweenObjects(panelSettingButton, panelSettingButton.GetComponent<PanelScript> ().startPosition, true ,false);
		TweenObjects(panelTitle, panelTitle.GetComponent<PanelScript> ().startPosition, true ,false);
		TweenObjects(panelTitleIcons, panelTitleIcons.GetComponent<PanelScript> ().startPosition, true ,false);
		TweenObjects(panelGameOver, panelGameOver.GetComponent<PanelScript> ().startPosition, true ,false);

		//TweenObjects(panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().startPosition, false,true);
	}
	public void HideShop ()
	{
		panelStore.transform.DOKill ();
		TweenObjects(panelStore, panelStore.GetComponent<PanelScript> ().startPosition, false,true);
	}

	public void PauseGame()
	{
		gameInterupted = true;
		if (GameManager.Instance.tutorialPlayed) {
			if (bannerLoaded) {
				AdMobManager.Instance.HideBanner ();
			}
			PieceGenerator.instance.goingBackToMenu = true;
			PieceGenerator.instance.startingGame_forStar = true;
			homeButtonOption = 0;

			panelPaused.transform.DOKill ();
			panelHomeButton.transform.DOKill ();

			if (PieceGenerator.instance.isHiding == false) {
				if (GameManager.Instance.selectedPiece == null) {
					//AdMobManager.Instance.HideBanner ();
					GameManager.Instance.gamePaused = true;
					panelPaused.SetActive (true);
					//button.interactable = false;
					TweenObjects (panelPaused, panelPaused.GetComponent<PanelScript> ().screenPosition, false, true);
					TweenObjects (panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().screenPosition, false, false);
					panelPausedButton.SetActive (false);
				}
			}
		}
	}
	public void ResumeGame()
	{	
		if (bannerLoaded) {
			AdMobManager.Instance.ShowBanner ();
		}
		panelPaused.transform.DOKill ();
		panelHomeButton.transform.DOKill ();
		panelPausedButton.transform.DOKill ();

		Debug.Log ("Tweening panelPaused");
		GameManager.Instance.gamePaused = false;
		button.interactable = true;
		TweenObjects(panelPaused, panelPaused.GetComponent<PanelScript> ().startPosition, true,true);
		TweenObjects(panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true,false);
		TweenObjects(panelPausedButton, panelPausedButton.GetComponent<PanelScript> ().screenPosition, false,false);
		Debug.Log ("Tweening panelPaused");
		PieceGenerator.instance.startingGame_forStar = false;
	}
	public void GoToMainMenu(int fromPanel) //homeButtonOption   0 = Pause
	{
		GameManager.Instance.SavePersistance ();
		GameManager.Instance.ResetScore ();
		GameManager.Instance.gamePaused = true;
		PieceManager.instance.ClearPieces ();
		button.interactable = true;
		Grid.instance.ResetGrid ();
		PieceGenerator.instance.TakePiecesAway ();
	/*
	GameManager.Instance.gamePaused = false;

	button.interactable = true;
	Grid.instance.ResetGrid ();
	PieceGenerator.instance.HidePieces (false);
	*/
		GameManager.Instance.RefreshHighScore (GameManager.Instance.highscore);
		Grid.instance.Appear ();
		panelPaused.transform.DOKill ();
		panelHomeButton.transform.DOKill ();
		panelStore.transform.DOKill ();
		panelMainMenu.transform.DOKill ();
		panelSettingButton.transform.DOKill ();
		panelTitle.transform.DOKill ();
		panelTitleIcons.transform.DOKill ();
		panelGameOver.transform.DOKill ();
		
		
		TweenObjects(panelTitle, panelTitle.GetComponent<PanelScript> ().screenPosition, false, false);
		TweenObjects(panelSettingButton, panelPaused.GetComponent<PanelScript> ().screenPosition, false, false);
		TweenObjects(panelTitleIcons, panelTitleIcons.GetComponent<PanelScript> ().screenPosition, false, false);
		if (homeButtonOption == 0) {
			TweenObjects (panelPaused, panelPaused.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects(panelSettingButton, panelSettingButton.GetComponent<PanelScript> ().screenPosition, false, false);
			TweenObjects (panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().screenPosition, false, true);
			PieceGenerator.instance.TakePiecesAway ();
		}
		else if (homeButtonOption == 1) {
			TweenObjects (panelStore, panelStore.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().screenPosition, false, true);
		}
		else if (homeButtonOption == 2) {
			TweenObjects (panelSettings, panelSettings.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().screenPosition, false, true);
			//panelMainMenu.SetActive (true);
			
			GameManager.Instance.gamePaused = false;
		}
		else if (homeButtonOption == 3) {
			TweenObjects (panelGameOver, panelGameOver.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().startPosition, true, true);
			TweenObjects (panelMainMenu, panelMainMenu.GetComponent<PanelScript> ().screenPosition, false, true);
		}
		

		PieceGenerator.instance.goingBackToMenu = true;
		PieceGenerator.instance.startingGame_forStar = true;
	}
	public void GameOver ()
	{
		endGameTime = DateTime.UtcNow;
		if (gameInterupted == false) {
			TimeSpan seconds = startGameTime.Subtract (endGameTime);
			//GameAnalytics.NewDesignEvent ("Game Lasted These Seconds", (float)seconds.TotalSeconds);
		}
		gameInterupted = false;

		GameManager.Instance.gamesEverPlayed++;
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "Step6 First Game Finished");

	if(GameManager.Instance.gamesEverPlayed >= 50)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "50 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 40)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "40 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 30)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "30 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 20)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "20 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 10)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "10 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 5)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "5 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 3)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "3 GAMES COMPLETED");	
	}
	else if(GameManager.Instance.gamesEverPlayed >= 2)
	{
		//GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, "2 GAMES COMPLETED");
	}


	//GameAnalytics.NewProgressionEvent (GAProgressionStatus.S, "Step6 First Game Finished");
		if (bannerLoaded) {
			AdMobManager.Instance.HideBanner ();
		}
		PieceGenerator.instance.goingBackToMenu = true;
		GameManager.Instance.gamePaused = true;
		homeButtonOption = 3;
		PieceGenerator.instance.TakePiecesAway ();

		panelGameOver.transform.DOKill ();
		panelTitleIcons.transform.DOKill ();
		panelPausedButton.transform.DOKill ();
		//panelHomeButton.transform.DOKill ();

		TweenObjects(panelTitleIcons, panelTitleIcons.GetComponent<PanelScript> ().startPosition, true,false);
		TweenObjects(panelPausedButton, panelPausedButton.GetComponent<PanelScript> ().startPosition, true,false);
		//TweenObjects(panelHomeButton, panelHomeButton.GetComponent<PanelScript> ().screenPosition, false,false);

		if (interAdLoaded) {
			AdMobManager.Instance.ShowInter(); 
		}
		
		//AdMobManager.Instance.HideBanner(); 
		//GameAnalyticsSDK.GameAnalytics.NewDesignEvent ("Score", GameManager.Instance.score);
		//GameAnalyticsSDK.GameAnalytics.NewProgressionEvent (GAProgressionStatus.Fail, "tutorial");
		GameManager.Instance.gamePaused = true;
		panelGameOver.SetActive (true);
		//button.interactable = false;
		TweenObjects(panelGameOver, panelGameOver.GetComponent<PanelScript> ().screenPosition, false,true);
		GameManager.Instance.SavePersistance ();
	}



	public void ChangeMusicEffects()
	{
		if (musicEffectsON) 
		{
			musicEffectsON = false;
		} 
		else 
		{
			musicEffectsON = true;
		}
	}
		
	public void TweenObjects (GameObject objectToTween, Vector3 positionToTweenTo, bool setActiveFalse, bool bounce)
	{
		objectToTween.SetActive (true);
		int bounceInt = 0;
		
		if (bounce) {
			if (objectToTween.transform.position.y < positionToTweenTo.y) {
				bounceInt = 100;
			} else if (objectToTween.transform.position.y < positionToTweenTo.y) {
				bounceInt = -100;
			}
		}

		objectToTween.transform.DOLocalMoveY (positionToTweenTo.y + bounceInt, 0.5f, false).OnComplete (() => {
		if(bounce)
		{
			objectToTween.transform.DOLocalMoveY (positionToTweenTo.y, 0.3f, false).OnComplete (() => {
				if (setActiveFalse) {
					objectToTween.SetActive (false);
				}
			});
		}
		else
		{
			if (setActiveFalse) {
				objectToTween.SetActive (false);
			}
		}
		});
	}

void OnApplicationPause(bool pauseStatus)
	{
		if (gamesCounter > 0) {
			//GameAnalytics.NewDesignEvent ("GamesPerSession", gamesCounter);
			gamesCounter = 0;
		}
		if (sessionGold > 0) {
			//GameAnalytics.NewDesignEvent ("Gold Per Session", sessionGold);
			sessionGold = 0;
		}
		if (videosWatchedPerSession > 0) {
			//GameAnalytics.NewDesignEvent ("Videos Watched per Session", videosWatchedPerSession);
			videosWatchedPerSession = 0;
		}
	}
	public void LoadRewardedVideo()
	{
		//if (videoLoaded == false) {
		//	AdMobManager.Instance.LoadVideo ();
		//} 
	}
	IEnumerator LoadingVideo()
	{
		yield return new WaitForSeconds(1);
		//AdMobManager.Instance.LoadVideo ();
	}
}
