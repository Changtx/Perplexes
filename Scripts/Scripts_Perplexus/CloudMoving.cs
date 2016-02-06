using UnityEngine;
using System.Collections;

public class CloudMoving : MonoBehaviour {

	public float CloudSpeed;


	// Update is called once per frame
	void Update () 
	{
		if (transform.position.z >= 110 || transform.position.z <= -150) 
		{
			CloudSpeed = -CloudSpeed;
		}
		transform.position = transform.position + new Vector3 (0, 0, 0.1f * CloudSpeed * Time.deltaTime);
	}
}
