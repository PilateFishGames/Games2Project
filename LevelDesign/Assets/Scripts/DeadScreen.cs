using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour {

	public string level;
	public string title;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void retry() {
		SceneManager.LoadScene (level);
	}

	public void quit() {
		SceneManager.LoadScene (title);
	}
}
