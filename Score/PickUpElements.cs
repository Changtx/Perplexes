using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpElements : MonoBehaviour
{
	public int ItemValue = 10;
	/*****************************/
	private bool isinvalid = false;
	private GameObject Score;
	private AudioSource coinSound;
	
	void Start ()
	{
		Score = GameObject.FindWithTag("ScoreManager");
		//Debug.Log ("find ScoreManager");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (isinvalid || !other.CompareTag ("Player")) {
			return;
		}
		Score.SendMessage ("addScore", ItemValue, SendMessageOptions.DontRequireReceiver);
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