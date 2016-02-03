using UnityEngine;
using System.Collections;

public class ShowLeaderBoard : MonoBehaviour {
	public static ShowLeaderBoard SLB;
	// Use this for initialization

	void Awake(){
		SLB = this;
	}

	void Start(){
		CloseLB();
	}

	public void ShowLB(){
		this.gameObject.SetActive (true);
	}

	public void CloseLB(){
		this.gameObject.SetActive (false);
	}
}
