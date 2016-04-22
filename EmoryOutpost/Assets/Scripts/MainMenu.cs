using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;
	public string currentCheckpoint;
    public float holdTime;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ContinueGame() {

	}

	public void NewGame() {
        source.Play();
        StartCoroutine(StartGame());
	}

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(holdTime);
        //Application.LoadLevel(firstLevel);
        SceneManager.LoadScene(firstLevel);
    }

    public void ExitGame() {
        Application.Quit();
	}
}
