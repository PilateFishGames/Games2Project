using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;

public class EnemyController : MonoBehaviour {

	public float health = 100.0f;

	public float damage = 10.0f;

	public float speed = 5.0f;

	public float timeBetweenAttacks = 1.0f;

	private bool playerInRange;
	private float attackTimer;

	private PlayerController player;
	private Vector3 playerPosition;

	private NavMeshAgent nav;

	private Animator anim;

	//public float sinkSpeed = 2.5f;
	private bool isDead;
	//private bool isSinking;

	private CapsuleCollider cc;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		cc = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		playerPosition = player.transform.position;
		Vector3 direction = playerPosition - transform.position;
		OrientModel (direction);
		anim.SetFloat ("Speed", speed);
		//transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed * Time.deltaTime);
		*/
		if (health > 0 && player.currentHealth > 0) {
			nav.SetDestination (player.transform.position);
		} 
		else {
			nav.enabled = false;
		}

		attackTimer += Time.deltaTime;

		if (attackTimer >= timeBetweenAttacks && playerInRange && health > 0) {
			//anim.SetTrigger ("attack1");
			Attack ();
		}

		if (player.currentHealth <= 0) {
			anim.SetTrigger ("playerDead");
		}
		/*
		if (isSinking) {
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
		*/
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			playerInRange = true;
        }
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			playerInRange = false;
		}
	}

	public void Attack() {
		attackTimer = 0.0f;
		if (player.currentHealth > 0.0f) {
			player.takeDamage (damage);
		}
	}

	public void takeDamage(float damage) {
		if (isDead)
			return;
		health -= damage;
		//Debug.Log ("Enemy health: " + health);
		if (health <= 0) {
			Die ();
		} 
	}

	void Die() {
		isDead = true;
		cc.isTrigger = true;
		anim.SetTrigger ("isDead");
		nav.enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		Destroy (gameObject, 5.0f);
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
