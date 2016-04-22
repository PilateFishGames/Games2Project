using UnityEngine;
using System.Collections;

public class HorrorController : MonoBehaviour {

    public float damage;
    public float attackDelay;
    public static bool followPlayer;
    public static bool playFollow;

    private bool playerInRange;

    public Transform homePoint;
    public AudioClip followClip;
    public AudioClip attackClip;

    private PlayerController player;
    private NavMeshAgent nav;
    private Animation anim;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        source = GetComponent<AudioSource>();
        followPlayer = false;
        playFollow = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (followPlayer)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.SetDestination(homePoint.position);
        }

        if (!playerInRange)
        {
            anim.Play("idle");
        }
        else if(!anim.IsPlaying("attack"))
        {
            StartCoroutine(TimeAttack());
            anim.Play("attack");
        }

        if (playFollow)
        {
            source.clip = attackClip;
            source.Play();
            playFollow = false;
        }

        StartCoroutine(OrientModel());
	}

    IEnumerator OrientModel()
    {
        yield return new WaitForEndOfFrame();
        transform.localRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Trigger Entered");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Trigger Exited");
            playerInRange = false;
        }
    }

    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        Attack();
    }

    void Attack()
    {
        if(player.currentHealth > 0.0f && playerInRange)
        {
            source.clip = attackClip;
            source.Play();
            player.takeDamage(damage);
        }
    }
}
