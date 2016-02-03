using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour {
	public static SaveLoadManager SLManager;
	void Awake(){
		SLManager = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

		SaveManager saver = new SaveManager ();

		if (ScoreManager.PlayerS.playerScores == null) {
			Debug.Log("player iformation lost");
			return;
		}

		saver.playerScores = ScoreManager.PlayerS.playerScores;

		bf.Serialize (file, saver);
		file.Close ();


	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/PlayerData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
			SaveManager saver = (SaveManager)bf.Deserialize (file);
			file.Close ();

			ScoreManager.PlayerS.playerScores = saver.playerScores;
		}
	}

}
[Serializable]
class SaveManager{
	public Dictionary<int, Dictionary<string, int>> playerScores;
}




