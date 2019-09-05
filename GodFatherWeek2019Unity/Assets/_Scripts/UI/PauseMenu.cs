using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject playMenuUI;
	
	public void LoadMenu()
	{
		Debug.Log ("Loading menu ...");

		Time.timeScale = 1f;

		SceneManager.LoadScene ("MainMenu");
	}

	public void QuitGame()
	{
		Debug.Log ("Quitting game ...");

		Application.Quit ();
	}

	public void RestartLevel()
	{
		Debug.Log ("Restarting level ...");

		Time.timeScale = 1f;

		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
