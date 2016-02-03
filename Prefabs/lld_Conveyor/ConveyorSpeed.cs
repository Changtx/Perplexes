using UnityEngine;
using System.Collections;

public class ConveyorSpeed : MonoBehaviour
{
	public float speed = 5f;
	public float friction = 0.8f;
	/***************************/
	private float oldspeed = 0f;
	private float currspeed = 0f;
	private BeltController belt;
	private WheelController leftwheel;
	private WheelController rightwheel;
	/*******************************/

	void Start ()
	{
		belt = transform.FindChild ("Belt").GetComponent<BeltController> ();
		leftwheel = transform.FindChild ("left_wheel").GetComponent<WheelController> ();
		rightwheel = transform.FindChild ("right_wheel").GetComponent<WheelController> ();
		belt.speed = currspeed;
		belt.friction = friction;
		leftwheel.speed = currspeed;
		leftwheel.friction = friction;
		rightwheel.speed = currspeed;
		rightwheel.friction = friction;
	}

	void Update ()
	{
		if (currspeed != speed) {
			currspeed += Mathf.Max (-0.1f, Mathf.Min (0.1f, speed - currspeed));
			belt.speed = currspeed;
			leftwheel.speed = currspeed;
			rightwheel.speed = currspeed;
		}
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