using UnityEngine;
using System.Collections;

public class ScaleController : MonoBehaviour
{
	public bool auto = true;
	public float Scale = 2f;
	public float TimeHalt = 2f;
	public float TimeScaleUp = 1f;
	public float TimeScaleDown = 3f;
	/*************************/
	private float minScale;
	private float maxScale;
	private float Haltcount;
	private float ScaleUpVel;
	private float ScaleDownVel;

	void Start ()
	{
		minScale = Mathf.Min (transform.localScale.z, Scale);
		maxScale = Mathf.Max (transform.localScale.z, Scale);
		Haltcount = TimeHalt;
		ScaleUpVel = (maxScale - minScale) / TimeScaleUp;
		ScaleDownVel = (minScale - maxScale) / TimeScaleDown;
		Scale = transform.localScale.z;
	}

	void Update ()
	{
		if (Scale != transform.localScale.z) {
			float cache;
			if (Scale > transform.localScale.z) {
				cache = ScaleUpVel * Time.deltaTime;
				cache = Mathf.Min (cache, Scale - transform.localScale.z);
			} else {
				cache = ScaleDownVel * Time.deltaTime;
				cache = Mathf.Max (cache, Scale - transform.localScale.z);
			}
			transform.position += transform.forward * cache * 0.5f;
			transform.localScale += new Vector3 (0f, 0f, cache);
		} else if (auto) {
			if (Haltcount <= 0f) {
				Haltcount = TimeHalt;
				Scale = (Scale == minScale) ? maxScale : minScale;
			} else {
				Haltcount -= Time.deltaTime;
			}
		}
	}

	void OnPause ()
	{
		auto = false;
	}

	void OnRestart ()
	{
		auto = true;
	}

	void OnReverse ()
	{
		Scale = (Scale == minScale) ? maxScale : minScale;
	}
}