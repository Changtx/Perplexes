using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootBolt : MonoBehaviour
{
	public bool auto = false;
	public Transform SpawnPos;
	public float boltSpeed = 50f;
	public float reloadTime = 1f;
	public float minfirePrepare = 2f;
	public float maxfirePrepare = 4f;
	public List<GameObject> bolts;
	/*********************/
	private GameObject reload = null;
	private float nextReload;
	private float nextFire;
	private AudioSource fireSound;
	private ParticleSystem explosion;

	void Awake ()
	{
		fireSound = GetComponent<AudioSource> ();
		explosion = GetComponentInChildren<ParticleSystem> ();
	}

	void Update ()
	{
		if (reload) {
			if (Time.time >= nextFire) {
				reload.GetComponent<Rigidbody> ().isKinematic = false;
				reload.GetComponent<Rigidbody> ().velocity = SpawnPos.up * boltSpeed;
				nextReload = Time.time + reloadTime;
				reload = null;
				fireSound.Play ();
				explosion.Play ();
			}
		} else if (auto) {
			if (Time.time >= nextReload) {
				int index = Random.Range (0, bolts.Count);
				reload = Instantiate (bolts [index], SpawnPos.position, SpawnPos.rotation) as GameObject;
				reload.transform.SetParent (SpawnPos);
				nextFire = Time.time + Random.Range (minfirePrepare, maxfirePrepare);
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
}