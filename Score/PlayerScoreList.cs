using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreList : MonoBehaviour
{

	public GameObject playerScoreEntryPrefab;
	public int level;
	int lastChangeCounter;

	// Use this for initialization
	void Start ()
	{

		lastChangeCounter = ScoreManager.PlayerS.GetChangeCounter () + 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//this.gameObject.SetActive(true);
		if (ScoreManager.PlayerS.GetChangeCounter () == lastChangeCounter) {
			//no change since last update
			return;
		}
		//data has changed
		lastChangeCounter = ScoreManager.PlayerS.GetChangeCounter ();
		while (this.transform.childCount >0) {
			Transform c = this.transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		string[] names = ScoreManager.PlayerS.GetPlayerNames ();
		int i = 0;
		foreach (string name in names) {
			//Debug.Log(name);
			i++;
			if (i < 9) {
				GameObject go = (GameObject)Instantiate (playerScoreEntryPrefab);
				go.transform.SetParent (this.transform);
				go.transform.Find ("Rank").GetComponent<Text> ().text = i.ToString ();
				go.transform.Find ("Username").GetComponent<Text> ().text = name;
				go.transform.Find ("Score").GetComponent<Text> ().text = ScoreManager.PlayerS.GetScore (name, level).ToString () + " Sec";
			}
		}
		while (i < 9) {
			i++;
			GameObject go = (GameObject)Instantiate (playerScoreEntryPrefab);
			go.transform.SetParent (this.transform);
			go.transform.Find ("Rank").GetComponent<Text> ().text = i.ToString ();
		}

	}
}
