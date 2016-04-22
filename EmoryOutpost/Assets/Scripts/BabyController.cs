using UnityEngine;
using System.Collections;

public class BabyController : MonoBehaviour {

    public float health;
    public static bool playerInSight;

    private bool followPlayer;
    private float baseSpeed;
    private bool isDead;

    public GameObject insectImpact;

    private NavMeshAgent nav;
    private GameObject[] movePoints;
    private PlayerController player;
    private NavMeshHit hitInfo;
    private AudioSource source;
    private GameController gc;
    private MeshCollider mc;

    // Use this for initialization
    void Start () {
        nav = transform.parent.GetComponent<NavMeshAgent>();
        movePoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        player = FindObjectOfType<PlayerController>();
        followPlayer = false;
        source = GetComponent<AudioSource>();
        baseSpeed = nav.speed;
        gc = FindObjectOfType<GameController>();
        mc = GetComponentInChildren<MeshCollider>();
        playerInSight = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 0 && player.currentHealth > 0)
        {
            if (!nav.Raycast(player.transform.position, out hitInfo))
            {
                if (!followPlayer)
                {
                    gc.SpawnInitialArmored();
                    playerInSight = true;
                }
                followPlayer = true;
                nav.speed = baseSpeed * 1.5f;
            }

            if (followPlayer)
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
    }

    public void takeDamage(float damage, Vector3 hitPoint)
    {
        if (isDead) return;
        Instantiate(insectImpact, hitPoint, Quaternion.identity);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else if (!followPlayer)
        {
            nav.SetDestination(player.transform.position);
            followPlayer = true;
            nav.speed = baseSpeed * 2.0f;
        }
    }

    void Die()
    {
        isDead = true;
        mc.isTrigger = true;
        //rb.useGravity = true;
        //rb.isKinematic = true;
        GetComponent<Animation>().enabled = false;
        nav.enabled = false;
        playerInSight = false;
        GameController.babyAlive = false;
        Destroy(gameObject);
    }
}
