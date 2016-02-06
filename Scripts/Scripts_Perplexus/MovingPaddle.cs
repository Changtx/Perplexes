using UnityEngine;
using System.Collections;

public class MovingPaddle : MonoBehaviour {

    bool right;
	public float Speed;

	void Start () 
	{
        right = true;
	}
	void FixedUpdate () 
	{
        if (right)
        {
            transform.Translate(new Vector3(1,0,0) * Time.deltaTime * Speed);
        }
        else
        {
			transform.Translate(new Vector3(-1,0,0) * Time.deltaTime * Speed);
        }
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log (other.gameObject.name);
		
		if (other.gameObject.tag != "Player") {
			right = !right;
		}
	}
}
