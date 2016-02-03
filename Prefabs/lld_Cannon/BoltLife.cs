using UnityEngine;
using System.Collections;

public class BoltLife : MonoBehaviour
{
	public float liveLife = 30.0f;
	public float dyingLife = 3.0f;
	private float startdieTime;
	private Vector3 scale;
	private float mass;

	void Start ()
	{
		startdieTime = Time.time + liveLife;
		scale = transform.localScale;
		mass = GetComponent<Rigidbody> ().mass;
	}

	void Update ()
	{
		float percent = Time.time - startdieTime;
		if (percent > 0.0f) {
			percent = 1.0f - (percent / dyingLife);
			if (percent < 0.1f) {
				Destroy (gameObject);
				return;
			}
			transform.localScale = scale * percent;
			GetComponent<Rigidbody> ().mass = mass * percent;
		}
	}
}