using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using DemiumGames.Saveable;
using DemiumGames.AdMobManager;

public enum StateOfTutorial 
{
	Animation, Movement, Complete
}

	

public class GameManager : MonoBehaviour 
{
	public bool gamePaused = true;
	public bool tutorialPlayed = false;
	public int tutorialCurrentPieceIndex;
	public StateOfTutorial stateTutorial;
	public bool interLoaded = true;
	public Persistance persistance; //DON'T ASIGN PUBLIC OBJECT!
	public int gamesEverPlayed;
	public int videosEverWatched;
	public int numberOfBoughtSkins;

	private static GameManager instance = null;
	public Piece selectedPiece = null;
	public Vector2 lastSelectedPosition;

	public Vector2 firstBlockPos0coma0;

	public GameObject uiScore;
	TextMeshProUGUI uiScoreText;

	public GameObject uiHighscore;
	TextMeshProUGUI uiHighscoreText;

	public GameObject uiCurrency;
	TextMeshProUGUI uiCurrencyText;

	public GameObject uiMainScreenHighscore;
	TextMeshProUGUI uiMainScreenHighscoreText;

	public GameObject uiMainScreenScore;
	TextMeshProUGUI uiMainScreenScoreText;

	public GameObject uiMainScreenCurrency;
	TextMeshProUGUI uiMainScreenCurrencyText;

	public GameObject StoreCurrency;
	public TextMeshProUGUI StoreCurrencyText;

	public int selectedSkinIndex; 

	public int score;
	public int highscore;


	public Sprite[] sprites;
	public Sprite spriteZeroAlpha;
	public static GameManager Instance
	{
		get
		{ 
			return instance; 
		}
		set
		{
			instance = value;
		}
	}
	void Awake()
	{
		instance = this;
		uiScoreText = uiScore.GetComponent<TextMeshProUGUI> ();
		uiHighscoreText = uiHighscore.GetComponent<TextMeshProUGUI> ();
		uiCurrencyText = uiCurrency.GetComponent<TextMeshProUGUI> ();
		uiMainScreenHighscoreText = uiMainScreenHighscore.GetComponent<TextMeshProUGUI> ();
		uiMainScreenCurrencyText = uiMainScreenCurrency.GetComponent<TextMeshProUGUI> ();
		uiMainScreenScoreText = uiMainScreenScore.GetComponent<TextMeshProUGUI> ();
		StoreCurrencyText = StoreCurrency.GetComponent<TextMeshProUGUI> ();
	}
	// Use this for initialization
	void Start () {
		persistance = new Persistance ();
		highscore = persistance.highscore;
		RefreshHighScore (highscore);

	}
	public void ResetScore()
	{
		score = 0;
		uiScoreText.text = "" + score;
	}
	public void RefreshHighScore(int highscore)
	{
		uiHighscoreText.text = "" + highscore;
		uiMainScreenHighscoreText.text = "" + highscore;
		uiMainScreenCurrencyText.text = "" + Shop.instance.currency;
		uiCurrencyText.text = "" + Shop.instance.currency;
		StoreCurrencyText.text = "" + Shop.instance.currency;
		uiMainScreenScoreText.text = "" + score;
		MenuManager.instance.gameOverTextCurrency.GetComponent<TextMeshProUGUI>().text  = "" + Shop.instance.currency;
		MenuManager.instance.gameOverTextHighscore.GetComponent<TextMeshProUGUI>().text = "" + highscore;

		this.highscore = highscore;
	}
	public void SavePersistance()
	{
		persistance.SaveData ();
	}
	public void AddCurrency(int currencyToAdd)
	{
		MenuManager.instance.sessionGold += currencyToAdd;
		Shop.instance.currency += currencyToAdd;
		uiCurrencyText.text = "" + Shop.instance.currency;
		uiMainScreenCurrencyText.text = "" + Shop.instance.currency;
		SavePersistance ();
	}

	public void AddPoints(int pointsToAdd)
	{
		if (score > 15 && interLoaded == false) {
			interLoaded = true;
			AdMobManager.Instance.LoadInter ();
		}
		score += pointsToAdd;
		uiScoreText.text = "" + score;
		//PieceManager.instance.CheckGameOver ();

		//highscore = score;
		if (score > highscore) {
			highscore = score;
			if (gamesEverPlayed > 1) {
				GameAnalyticsSDK.GameAnalytics.NewDesignEvent("Highscore ", score);
			}
		}
		//persistance.highscore = score;
		uiHighscoreText.text = "" + highscore;
		uiMainScreenHighscoreText.text = "" + highscore;
		uiMainScreenScoreText.text = "" + score;
		uiCurrencyText.text = "" + Shop.instance.currency;
		uiMainScreenCurrencyText.text = "" + Shop.instance.currency;
		MenuManager.instance.gameOverTextCurrency.GetComponent<TextMeshProUGUI>().text  = "" + Shop.instance.currency;
		MenuManager.instance.gameOverTextHighscore.GetComponent<TextMeshProUGUI>().text = "" + highscore;

		//ADD money!!!


		SavePersistance ();
	}
}
