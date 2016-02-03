using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{

	public static ScoreManager PlayerS;
	public Dictionary<int, Dictionary<string, int>> playerScores;
	int changeCounter = 0;
	public string playerName;
	public int playerScore = 0;
	public int level;
	public float timeScore = 0;
	public int collectScore = 0;
	public int hiScore;
	public float pointPerSecond;
	public bool StartCountingTime = true;
	public  bool showWinWindow = false;
	private Rect windowRect = new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2);
	public GameObject Buttons;
	public Text ScoreOnWinText;
	public GameObject Inputname;
	public Text scoreText;
	public Text hiScoreText;
	private AudioSource coinSound;

	void Awake ()
	{
		PlayerS = this;
		coinSound = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		//playerScores = null;
		//playerScores = new Dictionary<int, Dictionary<string, int>> ();
		//SaveLoadManager.SLManager.Save ();
		//SaveLoadManager.SLManager.Load ();

		Init ();

		if (GetPlayerNames ().Length != 0) {
			string firstName = GetPlayerNames () [0];
			hiScore = GetScore (firstName, level);
		} else {
			hiScore = 9999;
		}
		hiScoreText.text = "High Score : " + hiScore + " Sec";
	}

	void Update ()
	{
		if (StartCountingTime && playerScore >= 0) {
			timeScore += pointPerSecond * Time.deltaTime;
		} else {
			playerScore = 0;
		}
		playerScore = Mathf.RoundToInt (timeScore) + collectScore;
		scoreText.text = "Score : " + playerScore + " Sec";

	}

	void OnWin ()
	{
		showWinWindow = true;
		Time.timeScale = 0;
	}

	void addScore (int addScore)
	{
		if (playerScore > addScore) {
			collectScore -= addScore;
		} else {
			collectScore = -Mathf.RoundToInt (timeScore);
		}
		coinSound.Play ();
	}

	void Init ()
	{
		SaveLoadManager.SLManager.Load ();
		if (playerScores != null) {
			if (playerScores.ContainsKey (level) != false) {
				//Debug.Log("exist");
				return;
			}
			playerScores [level] = new Dictionary<string, int> ();
			//Debug.Log("creating level score");

		} else {
			playerScores = new Dictionary<int, Dictionary<string, int>> ();
			playerScores [level] = new Dictionary<string, int> ();
			//Debug.Log("creating playerScores and level score");
		}
	}
	
	public int GetScore (string username, int level)
	{
		Init ();
		
		if (playerScores.ContainsKey (level) == false) {
			return -1;
		}
		
		if (playerScores [level].ContainsKey (username) == false) {
			return -1;
		}
		
		return playerScores [level] [username];
	}
	
	public void SetScore (string username, int level, int value)
	{
		SaveLoadManager.SLManager.Load ();
		Init ();
		changeCounter++;
		//Debug.Log (changeCounter);
		if (playerScores.ContainsKey (level) == false) {
			playerScores [level] = new Dictionary<string, int> ();
		}
		playerScores [level] [username] = value;
	}
	
	public void ChangeScore (string username, int level, int amount)
	{
		Init ();
		int currScore = GetScore (username, level);
		SetScore (username, level, currScore + amount);
	}
	
	public void SavePlayerScore ()
	{
		//set score to dictionary in scoremanager
		SetScore (playerName, level, playerScore);
		//save dictionary to binary file
		SaveLoadManager.SLManager.Save ();

	}

	void RestartLevel ()
	{
		showWinWindow = false;
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevel);
	}

	public string[] GetPlayerNames ()
	{
		SaveLoadManager.SLManager.Load ();
		Init ();
		return playerScores [level].Keys.OrderBy (userN => GetScore (userN, level)).ToArray ();
	}

	public int GetChangeCounter ()
	{
		return changeCounter;
	}

	public void DeleteHistory ()
	{
		playerScores [level] = new Dictionary<string, int> ();
		//save dictionary to binary file
		SaveLoadManager.SLManager.Save ();
		changeCounter++;
	}






	//Test Window
	void OnGUI ()
	{
		if (showWinWindow == true) {
			ShowLeaderBoard.SLB.ShowLB ();
			Buttons.SetActive (false);
			ScoreOnWinText.text = "Score: " + playerScore + " Sec";

			if(Inputname.GetComponent<Text> ().text != "")
			{
				playerName = Inputname.GetComponent<Text> ().text;
			}


			//windowRect = GUI.Window (0, windowRect, WindowContain, "Input Info");

			/*
			if (playerScore > PlayerPrefs.GetInt("HighScore")) {
				PlayerPrefs.SetInt("HighScore", playerScore);
			}
			*/
		}

	}

//	public void WindowContain (int windowID)
//	{
//		GUI.Label (new Rect (200, 40, 200, 35), "You Win!");
//		
//		GUI.Label (new Rect (200, 80, 100, 35), "Player Name: ");
//		playerName = GUI.TextField (new Rect (270, 80, 100, 20), playerName);
//		
//		GUI.Label (new Rect (200, 120, 200, 35), "Player Score: " + playerScore);
//		
//		if (GUI.Button (new Rect (200, 160, 200, 35), "Save Score")) {
//			//ShowLeaderBoard.SLB.ShowLB();
//			SavePlayerScore ();
//		}
//		
//		if (GUI.Button (new Rect (200, 200, 200, 35), "Delete History")) {
//			//ShowLeaderBoard.SLB.ShowLB();
//			DeleteHistory ();
//		}
//
//		if (GUI.Button (new Rect (200, 240, 200, 35), "Close Window")) {
//			showWinWindow = false;
//		}
//
//		
//		if (GUI.Button (new Rect (200, 280, 200, 35), "restart")) {
//			ShowLeaderBoard.SLB.CloseLB ();
//			RestartLevel ();
//		}
//	}
}

