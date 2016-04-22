using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{

    enum status {IN, WAITING, OUT, DONE};

    public Image foreImage;

    public float fadeSpeed;

    public float holdTime;

    public string titleScreen;

    private float alpha;

    private status fadeStatus;

    // Use this for initialization
    void Start()
    {
        alpha = 1.0f;
        fadeStatus = status.IN;
    }

    // Update is called once per frame
    void Update()
    {
        switch(fadeStatus)
        {
            case status.IN:
                if(alpha > 0.0f)
                {
                    alpha -= fadeSpeed * Time.deltaTime;
                    alpha = Mathf.Clamp01(alpha);
                    foreImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
                }
                else
                {
                    fadeStatus = status.WAITING;
                    StartCoroutine(FadeInDone());
                }
                break;
            case status.WAITING:
                break;
            case status.OUT:
                if(alpha < 1.0f)
                {
                    alpha += fadeSpeed * Time.deltaTime;
                    alpha = Mathf.Clamp01(alpha);
                    foreImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
                }
                else
                {
                    fadeStatus = status.DONE;
                }
                break;
            case status.DONE:
                SceneManager.LoadScene(titleScreen);
                break;
            default:
                break;
        }    
    }

    IEnumerator FadeInDone()
    {
        yield return new WaitForSeconds(holdTime);
        fadeStatus = status.OUT;
    }
}
