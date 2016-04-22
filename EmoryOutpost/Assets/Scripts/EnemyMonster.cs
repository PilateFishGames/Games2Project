using UnityEngine;
using System.Collections;

public class EnemyMonster : MonoBehaviour {

    public float health;
    public float damage;
    public float baseRange;
    public float timeForPlayerEscape;
    public float attackTime;

    private bool playerInRange;
    private float hearingRange;
    private bool followPlayer;
    private float timeLeftForEscape;

    public AudioClip followClip;
    public AudioClip attackClip;

    private Animation anim;
    private PlayerController player;
    private NavMeshAgent nav;
    private GameObject[] movePoints;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        player = FindObjectOfType<PlayerController>();
        nav = GetComponent<NavMeshAgent>();
        movePoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        followPlayer = false;
        timeLeftForEscape = 0;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.currentHealth > 0)
        {
            switch(player.walk)
            {
                case PlayerController.WalkStatus.RUNNING:
                    hearingRange = baseRange * 1.5f;
                    break;
                case PlayerController.WalkStatus.SNEAKING:
                    hearingRange = 0.0f;
                    break;
                case PlayerController.WalkStatus.WALKNG:
                    if (PlayerController.isWalking)
                    {
                        hearingRange = baseRange;
                    }
                    else
                    {
                        hearingRange = 0.0f;
                    }
                    break;
                default:
                    break;
            }

            if (FindPathLength() <= hearingRange)
            {
                if (!followPlayer)
                {
                    source.clip = followClip;
                    source.Play();
                }
                followPlayer = true;
                timeLeftForEscape = timeForPlayerEscape;
            }
            else if (timeLeftForEscape <= 0)
            {
                followPlayer = false;
            }
            else
            {
                timeLeftForEscape -= Time.deltaTime;
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

        if(!playerInRange)
        {
            anim.Play("walk");
        }

        if (player.currentHealth <= 0)
        {
            anim.Play("idle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger entered");
            playerInRange = true;
            StartCoroutine(TimeAttack());
            anim.Play("attack");
        }
    }

    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        Attack();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger exited");
            playerInRange = false;
            
        }
    }

    void Attack()
    {
        if (player.currentHealth > 0.0f && playerInRange)
        {
            source.clip = attackClip;
            source.Play();
            player.takeDamage(damage);
        }
    }

    public void takeNotice()
    {
        source.clip = followClip;
        source.Play();
        followPlayer = true;
        timeLeftForEscape = timeForPlayerEscape;
    }

    public float FindPathLength()
    {
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled) nav.CalculatePath(player.transform.position, path);

        Vector3[] points = new Vector3[path.corners.Length + 2];
        points[0] = transform.position;
        points[points.Length - 1] = player.transform.position;

        for(int i = 0; i < path.corners.Length; i++)
        {
            points[i + 1] = path.corners[i];
        }

        float pathLength = 0.0f;

        for(int i = 0; i < points.Length - 1; i++)
        {
            pathLength += Vector3.Distance(points[i], points[i + 1]);
        }

        return pathLength;
    }
}
