using UnityEngine;
using System.Collections;

public class ElasticityController : MonoBehaviour
{
	public float bouncy = 1.2f;
	public float maxspeed = 90f;
	/***************************/
	private Transform icon;
	private Vector3 oldScale;
	private Vector3 ScaleFactor;
	private int bouncyOccur = 0;
	private AudioSource bouncySound;

	void Awake ()
	{
		icon = transform.FindChild ("Icon");
		oldScale = icon.localScale;
		ScaleFactor = new Vector3 (1f / transform.localScale.x, 1f / transform.localScale.y, 1f / transform.localScale.z);
		bouncySound = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (bouncyOccur > 0) {
			--bouncyOccur;
			icon.localScale = oldScale + ScaleFactor * bouncyOccur * 0.15f;
		}
	}

	void OnCollisionEnter (Collision other)
	{
		bouncySound.Play ();
		Vector3 normal = Vector3.zero;
		foreach (ContactPoint contact in other.contacts) {
			normal += contact.point;
		}
		normal = Vector3.Normalize (other.transform.position * other.contacts.Length - normal);
		float speedtogo = Vector3.Dot (normal, other.relativeVelocity) * (-bouncy);
		if (speedtogo > 0f) {
			other.rigidbody.velocity = Vector3.ClampMagnitude (other.rigidbody.velocity + normal * speedtogo, maxspeed);
		}
		bouncyOccur = 12;
	}
}