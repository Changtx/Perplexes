using UnityEngine;
using System.Collections;

public class TransprantWallLightController : MonoBehaviour {
	public Color flashingStartColor = Color.blue;
	public Color flashingEndColor = Color.cyan;
	public float flashingDelay = 2.5f;
	public float flashingFrequency = 2f;
	protected HighlightableObject ho;
	protected MeshRenderer m;

	void Awake()
	{
		ho = gameObject.AddComponent<HighlightableObject>();
		m = this.GetComponent<MeshRenderer> ();
	}

	// Use this for initialization
	void Start () {
		m.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.tag == "Player" || other.collider.CompareTag("Ball")) {
			m.enabled = true;
			ho.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
		}
	}
	void OnCollisionExit(Collision other) {
		if (other.collider.tag == "Player" || other.collider.CompareTag("Ball")) {
			//m.enabled = false;
			ho.Off ();
		}
	}
}
