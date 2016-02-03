using UnityEngine;
using System.Collections;

public class lld_PlayerSounds : MonoBehaviour
{
	private AudioSource collisionSound;

	void Awake ()
	{
		collisionSound = GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter (Collision other)
	{
		if (!other.gameObject.CompareTag ("NoSound")) {
			float cache = other.relativeVelocity.sqrMagnitude;
			collisionSound.volume = 0.1f * cache / (cache + 100f);
			collisionSound.Play ();
		}
	}

	void OnWin ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}