using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float health;
	public float damage;
    public float enemySightAngle;
    public float timeForPlayerEscape;
    public static bool swarmPlayer;

    private bool playerInRange;
    private bool isDead;
    private float baseSpeed;
    private bool followPlayer;
    private float timeLeftForEscape;

    public GameObject bloodEffect;
    public AudioClip followClip;
    public AudioClip attackClip;

    private PlayerController player;
	private Vector3 playerPosition;
	private NavMeshAgent nav;
	private Animator anim;
    private CapsuleCollider cc;
    private GameObject[] movePoints;
    private NavMeshHit hitInfo;
    private AudioSource source;
    private EnemyMonster monster;
    private Vector3 turnSpeed;

    // Use this for initialization
    void Start () {
		player = FindObjectOfType<PlayerController> ();
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
        baseSpeed = nav.speed;
        cc = GetComponent<CapsuleCollider>();
        movePoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        followPlayer = false;
        timeLeftForEscape = 0;
        source = GetComponent<AudioSource>();
        monster = FindObjectOfType<EnemyMonster>();
    }
	
	// Update is called once per frame
	void Update () {
		if (health > 0 && player.currentHealth > 0) {
            Vector3 direction = transform.position - player.transform.position;
            if(Vector3.Angle(Vector3.forward, direction) <= enemySightAngle * 0.5f)
            {
                if (!nav.Raycast(player.transform.position, out hitInfo))
                {
                    if (!followPlayer)
                    {
                        source.clip = followClip;
                        source.Play();
                    }
                    followPlayer = true;
                    nav.speed = baseSpeed * 1.5f;
                    timeLeftForEscape = timeForPlayerEscape;
                }
            }
            else if (timeLeftForEscape <= 0)
            {
                followPlayer = false;
                nav.speed = baseSpeed;
            }
            else
            {
                timeLeftForEscape -= Time.deltaTime;
            }

            if (followPlayer || swarmPlayer)
            {
                nav.SetDestination(player.transform.position);
            }
            else if (nav.remainingDistance <= nav.stoppingDistance)
            {
                int spawnIndex = Random.Range(0, movePoints.Length);
                nav.SetDestination(movePoints[spawnIndex].transform.position);
            }
		} 
		else {
			nav.enabled = false;
		}
        /*
		if (player.currentHealth <= 0) {
			anim.SetTrigger ("playerDead");
		}
        */
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
            playerInRange = true;
            anim.SetBool("attack", true);
        }
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
            playerInRange = false;
            anim.SetBool("attack", false);
		}
	}

	public void Attack() {
		if (player.currentHealth > 0.0f && playerInRange) {
            source.clip = attackClip;
            source.Play();
			player.takeDamage (damage);
		}
	}

	public void takeDamage(float damage, Vector3 hitPoint) {
		if (isDead)
			return;
        Instantiate(bloodEffect, hitPoint, Quaternion.identity);
		health -= damage;
		if (health <= 0) {
			Die ();
		}
        else if(!followPlayer) {
            nav.SetDestination(player.transform.position);
            source.clip = followClip;
            followPlayer = true;
            nav.speed = baseSpeed * 1.5f;
            source.Play();
        } 
	}

	void Die() {
        GameController.enemyNum++;
        monster.takeNotice();
		isDead = true;
		cc.isTrigger = true;
		anim.SetTrigger ("isDead");
		nav.enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		Destroy (gameObject, 5.0f);
	}
    /*
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
    */
}
