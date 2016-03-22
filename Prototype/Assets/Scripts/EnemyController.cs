using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health = 100.0f;

	public float damage = 10.0f;

	public float speed = 5.0f;

	private PlayerController player;
	private Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = player.transform.position;
		Vector3 direction = playerPosition - transform.position;
		OrientModel (direction);
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().dealDamage(damage);
        }
	}

	public void dealDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	void OrientModel(Vector3 direction) {
		float absX = Mathf.Abs (direction.x);
		float absZ = Mathf.Abs (direction.z);
		float total = absX + absZ;

		float zPercent = absZ / total;

		float yRotPercent = zPercent * 90;

		float yRot;
		if (direction.z < 0) {
			yRot = yRotPercent + 90;
		} 
		else {
			yRot = 90 - yRotPercent;
		}

		if (direction.x < 0) {
			yRot = yRot * -1;
		}

		transform.rotation = Quaternion.Euler (transform.rotation.x, yRot, transform.rotation.z);
	}
}
