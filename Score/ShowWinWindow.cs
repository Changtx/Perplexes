using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowWinWindow : MonoBehaviour
{
	/*****************************/
	private bool isinvalid = false;
	private GameObject Score;
	
	void Start ()
	{
		Score = GameObject.FindWithTag("ScoreManager");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (isinvalid || !other.CompareTag ("Player")) {
			return;
		}
		Score.SendMessage ("OnWin", SendMessageOptions.DontRequireReceiver);
		this.gameObject.SetActive (false);
	}
	
	void OnTriggerExit (Collider other)
	{
		
	}
	
	void OnPause ()
	{
		isinvalid = true;
	}
}