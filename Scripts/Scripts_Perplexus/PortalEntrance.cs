using UnityEngine;
using System.Collections;

public class PortalEntrance : MonoBehaviour 
{
	public GameObject Des;

    void OnTriggerEnter(Collider other)
    {
		if (!other.CompareTag ("Player")) 
        {
			return;
        }
		other.gameObject.transform.position = Des.transform.position + Des.transform.up * 0.5f;
	}
}
