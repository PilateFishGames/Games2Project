using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public string mainMenu;

	public string controls;

	private bool isPaused;

	public GameObject pauseMenuCanvas;

	public GameObject target;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			isPaused = !isPaused;
		}

		if (isPaused) {
			pauseMenuCanvas.SetActive(true);
			Time.timeScale = 0.0f;
			target.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
			GameController.followMouse = false;

		} 
		else {
			pauseMenuCanvas.SetActive(false);
			Time.timeScale = 1.0f;
			target.SetActive(true);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
			GameController.followMouse = true;
        }
	}

	public void resume() {
		isPaused = false;
	}

	public void controlsButton() {
		//Application.LoadLevel (controls);
		SceneManager.LoadScene (controls);
	}

	public void quit() {
		//Application.LoadLevel (mainMenu);
		SceneManager.LoadScene (mainMenu);
	}
}
