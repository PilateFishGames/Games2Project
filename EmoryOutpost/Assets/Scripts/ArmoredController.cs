using UnityEngine;
using System.Collections;

public class ArmoredController : MonoBehaviour {

    public float health;
    public float damage;
    public float attackTime;
    public float enemySightAngle;
    public bool playerInRange;

    private bool isDead;
    private bool hasTarget;
    private static bool followPlayer;

    public GameObject insectImpact;

    private Animation anim;
    private NavMeshAgent nav;
    private MeshCollider mc;
    private PlayerController player;
    private AudioSource source;
    private Vector3 targetPosition;
    private GameObject[] movePoints;
    private NavMeshHit hitInfo;

	// Use this for initialization
	void Start () {
        isDead = false;
        anim = GetComponent<Animation>();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        mc = GetComponentInChildren<MeshCollider>();
        player = FindObjectOfType<PlayerController>();
        source = GetComponent<AudioSource>();
        movePoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        hasTarget = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(health > 0 && player.currentHealth > 0)
        {
            Vector3 direction = transform.position - player.transform.position;
            if (Vector3.Angle(Vector3.forward, direction) <= enemySightAngle * 0.5f)
            {
                if (!nav.Raycast(player.transform.position, out hitInfo))
                {
                    followPlayer = true;
                }
            }
            else if (BabyController.playerInSight)
            {
                hasTarget = true;
                targetPosition = player.transform.position;
            }
            else if(nav.velocity.magnitude <= 0 && hasTarget)
            {
                hasTarget = false;
            }

            if (followPlayer)
            {
                nav.SetDestination(player.transform.position);
            }
            else if (hasTarget)
            {
                nav.SetDestination(targetPosition);
            }
            else if (nav.remainingDistance <= nav.stoppingDistance)
            {
                int spawnIndex = Random.Range(0, movePoints.Length);
                nav.SetDestination(movePoints[spawnIndex].transform.position);
            }
        }
        else
        {
            nav.enabled = false;
        }

        if (!playerInRange)
        {
            anim.Play("idle");
        }
        else if (!anim.IsPlaying("attack"))
        {
            StartCoroutine(TimeAttack());
            anim.Play("attack");
        }

        StartCoroutine(OrientModel());
    }

    IEnumerator OrientModel()
    {
        yield return new WaitForEndOfFrame();
        transform.localRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
        }
    }
    */
    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        Attack();
    }

    void Attack()
    {
        if(player.currentHealth > 0 && playerInRange)
        {
            source.Play();
            player.takeDamage(damage);
        }
    }

    public void takeDamage(float damage, Vector3 hitPoint)
    {
        Debug.Log("Taking Damage");
        if (isDead) return;
        Instantiate(insectImpact, hitPoint, Quaternion.identity);
        health -= damage;
        followPlayer = true;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        mc.isTrigger = true;
        anim.enabled = false;
        nav.enabled = false;
        Destroy(gameObject);
    }
}
