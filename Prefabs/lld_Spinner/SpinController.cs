using UnityEngine;
using System.Collections;

public class SpinController : MonoBehaviour
{
	public float speed = 40f;
	private float oldspeed = 0f;
	private float currspeed = 0f;
	
	void Update ()
	{
		if (currspeed != speed) {
			currspeed += Mathf.Max (-0.3f, Mathf.Min (0.3f, speed - currspeed));
		}
		transform.Rotate (0f, currspeed * Time.deltaTime, 0f);
	}

	void OnPause ()
	{
		if (speed != 0f) {
			oldspeed = speed;
			speed = 0f;
		}
	}
	
	void OnRestart ()
	{
		if (speed != oldspeed) {
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