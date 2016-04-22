using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

    public float textDelay;

    public Text text;

    private string[] strings;
    public string level;

    // Use this for initialization
    void Start() {
        strings = new string[4];
        strings[0] = "The Emory Outpost research ship has gone dark.";
        strings[1] = "The Emory was last reported to be researching several alien species found on a nearby planet.";
        strings[2] = "The ship, along with the creatures onboard must be destroyed.";
        strings[3] = "Board the ship, activate the emergency self-destruct, and escape before the ship explodes.";
        StartCoroutine(StartText());
    }

    IEnumerator StartText()
    {
        for (int i = 0; i < strings.Length; i++)
        {
            text.text = strings[i];
            yield return new WaitForSeconds(textDelay);
        }
        SceneManager.LoadScene(level);
    }
}
