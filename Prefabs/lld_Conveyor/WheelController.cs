using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{
	public float speed;
	public float friction;
	/**********************/
	private float radian;
	private Transform rotateIcon;
	private AudioSource frictionSound;

	void Start ()
	{
		Transform father = transform.parent;
		float distance = father.localScale.x;
		radian = Mathf.Min (distance, father.localScale.z);
		transform.Translate (transform.right * (distance - radian) * 0.5f, Space.World);
		transform.localScale = new Vector3 (radian / distance, transform.localScale.y, 1f);
		frictionSound = father.gameObject.GetComponent<AudioSource> ();
		rotateIcon = transform.FindChild ("Icon");
	}

	void Update ()
	{
		rotateIcon.Rotate (0f, Time.deltaTime * speed * 180f / (Mathf.PI * radian), 0f);
	}

	private void HandleCollision (Collision other)
	{
		Vector3 normal = Vector3.zero, direct;
		foreach (ContactPoint contact in other.contacts) {
			normal += contact.point;
		}
		normal = Vector3.Normalize (other.transform.position * other.contacts.Length - normal);
		direct = Vector3.Cross (gameObject.transform.up, normal);
		float speedtogo = speed - Vector3.Dot (direct, other.relativeVelocity);
		if (speedtogo != 0f) {
			float impulse = Vector3.Dot (normal, other.impulse) * friction / (other.rigidbody.mass * Mathf.Abs (speedtogo));
			other.rigidbody.velocity += direct * speedtogo * Mathf.Min (1f, impulse);
		}
	}

	void OnCollisionEnter (Collision other)
	{
		HandleCollision (other);
	}

	void OnCollisionStay (Collision other)
	{
		HandleCollision (other);
		if ((speed != 0f) && !frictionSound.isPlaying) {
			frictionSound.UnPause ();
		}
	}

	void OnCollisionExit ()
	{
		frictionSound.Pause ();
	}
}