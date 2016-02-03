using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerController : MonoBehaviour
{
	public List<GameObject> tasks;
	public List<string> startEvents;
	public List<string> enterEvents;
	public List<string> exitEvents;
	/*****************************/
	private AudioSource triggerSound;
	private Transform triggerButton;
	private bool isinvalid = false;

	void Start ()
	{
		triggerSound = GetComponent<AudioSource> ();
		triggerButton = transform.FindChild ("TriggerButton");
		for (int i = 0; i < startEvents.Count; ++i) {
			if (startEvents [i] != "") {
				tasks [i].SendMessage (startEvents [i], SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {
			if (triggerButton.gameObject.activeSelf) {
				triggerSound.Play ();
			}
			triggerButton.position -= transform.up * 1.2f;
			if (isinvalid) {
				return;
			}
			for (int i = 0; i < enterEvents.Count; ++i) {
				if (enterEvents [i] != "") {
					tasks [i].SendMessage (enterEvents [i], SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Player")) {
			triggerButton.position += transform.up * 1.2f;
			if (isinvalid) {
				return;
			}
			for (int i = 0; i < exitEvents.Count; ++i) {
				if (exitEvents [i] != "") {
					tasks [i].SendMessage (exitEvents [i], SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	void OnPause ()
	{
		isinvalid = true;
	}
}