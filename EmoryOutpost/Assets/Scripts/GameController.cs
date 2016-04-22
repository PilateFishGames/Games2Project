using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class GameController : MonoBehaviour {

    public bool firstSpawnType;
    public float spawnDistance;
    public float spawnDelay;
    public int enemiesToSpawn;
    public float spawnInterval = 5.0f;
    public static bool timerInitiated;
    public static int enemyNum;
    public static bool babyAlive;

    private float delayRemaining;
    private float intervalLeft;

    public GameObject enemy;
    public Camera cam;
    public Text healthText;
    public Transform computer;
    public GameObject armored;
    public GameObject baby;
    public Image damageImage;

    private PlayerController player;
    private GameObject[] spawnPoints;
    private BlurOptimized blur;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		player = FindObjectOfType<PlayerController>();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		healthText.text = player.currentHealth.ToString ();
        /*
		if (!firstSpawnType) {
			StartCoroutine (SpawnEnemies ());
		}
        */
        blur = cam.GetComponent<BlurOptimized>();
        EnemyController.swarmPlayer = false;
        enemyNum = enemiesToSpawn;
        babyAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (firstSpawnType) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				if(spawnPoints[i] != null) {
					Vector3 spawnPlayerDistance = spawnPoints [i].transform.position - player.transform.position;
					if (spawnPlayerDistance.magnitude <= spawnDistance) {
						Vector3 spawnCameraPoint = cam.WorldToViewportPoint (spawnPoints [i].transform.position);
						bool isOnScreen = spawnCameraPoint.z > -0.5 && spawnCameraPoint.x > -0.5 && spawnCameraPoint.x < 1.5 && spawnCameraPoint.y > -0.5 && spawnCameraPoint.y < 1.5;
						if (!isOnScreen) {
							Instantiate (enemy, spawnPoints [i].transform.position, enemy.transform.rotation);
							spawnPoints [i] = null;
						}
					}
				}
			}
		}
        */

        while (enemyNum > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnIndex].transform.position, enemy.transform.rotation);
            enemyNum--;
        }
        //healthText.text = player.currentHealth.ToString ();
        if (player.currentHealth <= 0) {
			SceneManager.LoadScene ("DeadScene");
		}
        float damagePercent = 1 - (player.currentHealth / player.startHealth);
        float blurAmount = 10 * damagePercent;
        float redAmount = 0.5f * damagePercent;
        blur.blurSize = blurAmount;
        damageImage.color = new Color(1.0f, 0.0f, 0.0f, redAmount);

        if (!babyAlive)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(baby, spawnPoints[spawnIndex].transform.position, baby.transform.rotation);
            babyAlive = true;
        }

        if (BabyController.playerInSight)
        {
            intervalLeft -= Time.deltaTime;
            if (intervalLeft <= 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(armored, spawnPoints[spawnIndex].transform.position, armored.transform.rotation);
                intervalLeft = spawnInterval;
            }
        }
    }

    public void SpawnInitialArmored()
    {
        for (int i = 0; i < 5; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(armored, spawnPoints[spawnIndex].transform.position, armored.transform.rotation);
        }
    }

    /*
	IEnumerator SpawnEnemies() {
		while (enemyNum > 0) {
			int spawnIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemy, spawnPoints [spawnIndex].transform.position, enemy.transform.rotation);
			enemyNum--;
			yield return new WaitForSeconds (spawnDelay);
		}
	}
    */
}
