using UnityEngine;
using System.Collections;

public class EndLevel3 : MonoBehaviour
{
	private int enterTime;
	private GameObject Score;
	
	void Start ()
	{
		enterTime = 0;
		Score = GameObject.FindWithTag ("ScoreManager");
	}
	
	void OnTriggerEnter (Collider trig)
	{
		Debug.Log("ahhahah");
		if (!trig.CompareTag("Player"))
			return;
		enterTime ++;
		if (enterTime == 4) {
			Score.SendMessage ("OnWin", SendMessageOptions.DontRequireReceiver);
			this.gameObject.SetActive (false);
			return;
		}
	}
}