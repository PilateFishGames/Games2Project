using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newGame() {
		SceneManager.LoadScene (firstLevel);
	}

	public void exitGame() {
		Application.Quit ();
	}
}
