using UnityEngine;
using System.Collections;

public class eatPumpkin : MonoBehaviour
{

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Player") {
			//destroy pumpkin
			Destroy (gameObject);
		}
	}
}
