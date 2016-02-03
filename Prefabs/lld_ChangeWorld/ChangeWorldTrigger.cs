using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeWorldTrigger : MonoBehaviour
{
	public List<GameObject> tasks;
	public List<string> triggerEvents;
	/*************************/
	private lld_MainController mainScript;
	private Transform newWorld;
	private AudioSource victorySound;

	void Awake ()
	{
		mainScript = GameObject.FindWithTag ("MainCamera").GetComponent<lld_MainController> ();
		newWorld = transform;
		while (newWorld && !newWorld.gameObject.CompareTag("World")) {
			newWorld = newWorld.parent;
		}
		victorySound = GetComponent<AudioSource> ();
	}

	void OnCollisionExit (Collision other)
	{
		if ((mainScript.world != newWorld) && other.gameObject.CompareTag ("Player")) {
			if (Vector3.Dot (other.transform.position - transform.position, transform.up) < 0f) {
				return;
			}
			victorySound.Play ();
			mainScript.world = newWorld;
			for (int i = 0; i < triggerEvents.Count; ++i) {
				if (triggerEvents [i] != "") {
					tasks [i].SendMessage (triggerEvents [i], SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}