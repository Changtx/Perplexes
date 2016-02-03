using UnityEngine;
using System.Collections;

public class Spining : MonoBehaviour
{
	private float oldspeed = 0;
	public float speed = 10;
	public int dir = 0;

	void Update ()
	{	
		switch (dir) {
		case 0:
			transform.Rotate (speed * Time.deltaTime, 0, 0); 
			break;
		case 1:
			transform.Rotate (0, speed * Time.deltaTime, 0); 
			break;
		case 2:
			transform.Rotate (0, 0, speed * Time.deltaTime); 
			break;
		default:
			break;
		}

	}

	void OnPause ()
	{
		if (speed != 0) {
			oldspeed = speed;
			speed = 0;
		}
	}

	void OnRestart ()
	{
		if (speed == 0) {
			speed = oldspeed;
		}
	}

	void OnReverse ()
	{
		if (speed != 0f) {
			speed = -speed;
		}
	}
}