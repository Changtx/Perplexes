using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
	private AudioSource triggerSound;
	private Transform triggerButton;
	void Start ()
	{
		triggerSound = GetComponent<AudioSource> ();
		triggerButton = transform.FindChild ("TriggerButton");
	}
	void OnTriggerEnter (Collider other)
	{
		triggerButton.position -= transform.up * 1.2f;
		if (!other.CompareTag ("Player")) 
		{
			return;
		}
		triggerSound.Play ();
		Application.LoadLevel ("Perplexus");
	}
}
