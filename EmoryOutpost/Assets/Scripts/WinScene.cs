using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour {

    public string firstLevel;
    public string titleMenu;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Replay()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void Exit()
    {
        SceneManager.LoadScene(titleMenu);
    }
}
