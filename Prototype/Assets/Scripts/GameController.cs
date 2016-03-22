using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private PlayerController player;
	public static bool followMouse;
	//public int spawnPointNum;
	public GameObject[] spawnPoints;
	public bool firstSpawnType;
	public GameObject enemy;
	public float spawnDistance;
	public Camera cam;
	public float spawnDelay;
	private float delayRemaining;
	public int enemyNum;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		//firstSpawnType = true;

		if (!firstSpawnType) {
			StartCoroutine ("SpawnEnemies");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (firstSpawnType) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				if(spawnPoints[i] != null) {
					Vector3 spawnPlayerDistance = spawnPoints [i].transform.position - player.transform.position;
					if (spawnPlayerDistance.magnitude <= spawnDistance) {
						Vector3 spawnCameraPoint = cam.WorldToViewportPoint (spawnPoints [i].transform.position);
						bool isOnScreen = spawnCameraPoint.z > 0 && spawnCameraPoint.x > 0 && spawnCameraPoint.x < 1 && spawnCameraPoint.y > 0 && spawnCameraPoint.y < 1;
						if (!isOnScreen) {
							Instantiate (enemy, spawnPoints [i].transform.position, enemy.transform.rotation);
							spawnPoints [i] = null;
						}
					}
				}
			}
		} 
	}

	IEnumerator SpawnEnemies() {
		while (enemyNum > 0) {
			int spawnIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemy, spawnPoints [spawnIndex].transform.position, enemy.transform.rotation);
			enemyNum--;
			yield return new WaitForSeconds (spawnDelay);
		}
	}
}
