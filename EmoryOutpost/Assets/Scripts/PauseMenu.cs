using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public string mainMenu;

	public string controls;

	public static bool isPaused;

	public GameObject pauseMenuCanvas;

	public GameObject target;

    private AudioSource gameSource;

	// Use this for initialization
	void Start () {
		isPaused = false;
        gameSource = FindObjectOfType<GameController>().GetComponent<AudioSource>();
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
			PlayerController.followMouse = false;
            gameSource.Stop();
		} 
		else {
			pauseMenuCanvas.SetActive(false);
			Time.timeScale = 1.0f;
			target.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
			PlayerController.followMouse = true;
            gameSource.Play();
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
