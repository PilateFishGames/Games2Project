using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float health = 100.0f;

	private float damage = 10.0f;

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
		float distance = direction.magnitude;
		direction = direction.normalized;
		float move = speed * Time.deltaTime;
		if (move > distance) move = distance;
		transform.Translate (direction * move);
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
}
