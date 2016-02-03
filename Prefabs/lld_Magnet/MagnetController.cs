using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour
{
	public float force = 100f;
	private float sqrRange;
	private AudioSource magnetSound;

	void Awake ()
	{
		sqrRange = Mathf.Max (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		sqrRange *= GetComponent<SphereCollider> ().radius;
		sqrRange *= sqrRange;
		magnetSound = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			magnetSound.Play ();
		}
	}

	void OnTriggerStay (Collider other)
	{
		Vector3 normal = transform.position - other.transform.position;
		float ratio = Mathf.Max (0f, (sqrRange / normal.sqrMagnitude - 1f) / (sqrRange - 1f));
		other.attachedRigidbody.AddForce (other.attachedRigidbody.mass * force * ratio * normal);
	}
}