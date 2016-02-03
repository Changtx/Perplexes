using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour
{
	public float force = 8.0f;
	private Renderer arrowRender;
	private AudioSource windSound;

	void Awake ()
	{
		arrowRender = GetComponentInChildren<Renderer> ();
		windSound = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		arrowRender.material.mainTextureOffset -= new Vector2 (Time.deltaTime * force * 0.1f, 0f);
	}

	void OnTriggerStay (Collider other)
	{
		other.attachedRigidbody.AddForce (transform.right * other.attachedRigidbody.mass * force);
		if (!windSound.isPlaying && other.gameObject.CompareTag ("Player")) {
			windSound.Play ();
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			windSound.Stop ();
		}
	}

	void OnReverse ()
	{
		transform.Rotate (new Vector3 (0f, 180f, 0f));
	}
}