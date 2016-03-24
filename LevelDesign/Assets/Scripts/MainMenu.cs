using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;
	public string currentCheckpoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void continueGame() {

	}

	public void newGame() {
		GameController.followMouse = true;
		Application.LoadLevel (firstLevel);
		//SceneManager.LoadScene (firstLevel);
	}

	public void exitGame() {
		Application.Quit ();
	}
}
