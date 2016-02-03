using UnityEngine;
using System.Collections;

public class DroidLookAt : MonoBehaviour {

	public Transform target;
	
	void Update ()
	{
		transform.LookAt(target);
	}
}
