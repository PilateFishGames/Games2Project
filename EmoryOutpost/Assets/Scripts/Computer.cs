using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Computer : MonoBehaviour {

    public float timerAmount;
    public float removeDelay;

    private bool canPress;

    public Text destructText;
    public Text countdownText;
    public Text messageText;

    private PlayerController player;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit") && canPress)
        {
            source.Play();
            EnemyController.swarmPlayer = true;
            destructText.enabled = false;
            countdownText.enabled = true;
            messageText.enabled = true;
            GameController.timerInitiated = true;
            StartCoroutine(RemoveTextWithDelay());
        }

        if (GameController.timerInitiated)
        {
            timerAmount -= Time.deltaTime;

            if(timerAmount > 0)
            {
                string numString = timerAmount.ToString();
                if (timerAmount < 10)
                {
                    countdownText.text = numString.Substring(0, 4);
                }
                else if (timerAmount < 100)
                {
                    countdownText.text = numString.Substring(0, 5);
                }
                else
                {
                    countdownText.text = numString.Substring(0, 6);
                }
            }

            if (timerAmount <= 0)
            {
                player.takeDamage(1000);
            }
        }
    }

    IEnumerator RemoveTextWithDelay()
    {
        yield return new WaitForSeconds(removeDelay);
        messageText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        destructText.enabled = true;
        canPress = true;
    }

    void OnTriggerExit(Collider other)
    {
        destructText.enabled = false;
        canPress = false;
    }
}
