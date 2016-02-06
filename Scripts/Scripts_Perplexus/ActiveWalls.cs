using UnityEngine;
using System.Collections;

public class ActiveWalls : MonoBehaviour {
	
	public void Active ()
	{
		transform.FindChild ("stockwall").gameObject.SetActive (true);
	}
}
