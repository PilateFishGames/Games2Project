using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour {

	public string level;
	public string title;
    public float holdTime;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Retry() {
        source.Play();
        StartCoroutine(RestartGame());
	}

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(holdTime);
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

	public void Quit() {
        source.Play();
        StartCoroutine(ExitGame());
	}

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(holdTime);
        //Application.LoadLevel(title);
        SceneManager.LoadScene(title);
    }
}
