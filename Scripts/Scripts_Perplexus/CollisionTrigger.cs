using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour
{
	public Transform policetape;
	public Transform bus;
	public Transform armgate;
	int collisionPolice;
	int collisionBro;
	int collisionArmy;

	void Start ()
	{
		collisionBro = 0;
		collisionArmy = 0;
		collisionPolice = 0;
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			switch (gameObject.name) {
			case "Police":
				{
					policetape.localScale = new Vector3 (0, 0, 0);
					collisionPolice ++;
				}
				break;
			case "Bro":
				{
					if (collisionBro == 0) {
						bus.position = bus.position + new Vector3 (-3f, 3f, 0);
					}
					collisionBro ++;
				}
				break;
			case "Army":
				{
					if (collisionBro == 0) {
						armgate.Rotate (90, 0, 0);
					}
					collisionArmy ++;
				}
				break;
			}
		}
	}
}
