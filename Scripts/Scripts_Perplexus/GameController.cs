using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public void Pause ()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}

	public void Reset ()
	{
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void GotoMainMenu (string level)
	{
		Time.timeScale = 1;
		Application.LoadLevel (level);
	}

}
