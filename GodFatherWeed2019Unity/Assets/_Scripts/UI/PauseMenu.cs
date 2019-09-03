using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject playMenuUI;
	public GameObject pauseMenuUI;
	public GameObject optionsMenuUI;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void Resume()
	{
		playMenuUI.SetActive (true);
		pauseMenuUI.SetActive (false);
		optionsMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Pause()
	{
		playMenuUI.SetActive (false);
		pauseMenuUI.SetActive (true);
		optionsMenuUI.SetActive (false);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

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
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		//Application.LoadLevel(Application.loadedLevel);
	}

	public void NextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1f;
	}

	public bool get_GameIsPaused()
	{
		return(GameIsPaused);
	}
}
