using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public enum WalkStatus { WALKNG, RUNNING, SNEAKING };

    public float baseSpeed = 5.0f;
	public float movementSpeed;
	public float mouseSensitivity = 5.0f;
    public float upDownRange = 60.0f;
    public float jumpSpeed = 20.0f;
    public float startHealth = 50;
    public float currentHealth;
    public float healDelay;
    public float healRate;
    public float baseInterval;
    public static bool followMouse;
    public static bool isWalking;

    private float verticalRotation = 0;
	private float verticalVelocity = 0;
    private float stepInterval;
    private float timeLeftForStep;

    public AudioClip hurtClip;
    public AudioClip[] footsteps;

    private CharacterController characterController;
    private AudioSource source;

    public WalkStatus walk;

    // Use this for initialization
    void Start () {
		characterController = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
		followMouse = true;
		currentHealth = startHealth;
        stepInterval = baseInterval;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButton("Fire2"))
        {
            movementSpeed = baseSpeed * 1.5f;
            stepInterval = baseSpeed / 1.5f;
            walk = WalkStatus.RUNNING;

        }
        else if(Input.GetButton("Fire3"))
        {
            movementSpeed = baseSpeed * 0.5f;
            stepInterval = Mathf.Infinity;
            walk = WalkStatus.SNEAKING;
        }
        else
        {
            movementSpeed = baseSpeed;
            stepInterval = baseInterval;
            walk = WalkStatus.WALKNG;
        }

		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		if (followMouse) {
			transform.Rotate (0, rotLeftRight, 0);
		}

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		if (followMouse) {
			Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);	
		}
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if(characterController.isGrounded && Input.GetButton("Jump")) {
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		
		speed = transform.rotation * speed;

        if (speed.magnitude > 0) isWalking = true;
        else isWalking = false;

        characterController.Move(speed * Time.deltaTime);

        if(characterController.velocity != Vector3.zero)
        {
            TakeStep();
        }
        else
        {
            timeLeftForStep = stepInterval;
        }
	}

    void TakeStep()
    {
        timeLeftForStep -= Time.deltaTime;
        if(timeLeftForStep <= 0)
        {
            int index = Random.Range(1, footsteps.Length);
            source.clip = footsteps[index];
            source.Play();
            footsteps[index] = footsteps[0];
            footsteps[0] = source.clip;
            timeLeftForStep = stepInterval;
        }    
    }

	public void takeDamage(float damage) {
        source.clip = hurtClip;
        source.Play();
		currentHealth -= damage;
        StopCoroutine(HealGraduallyAfterDelay());
        StartCoroutine(HealGraduallyAfterDelay());
	}

    IEnumerator HealGraduallyAfterDelay()
    {
        yield return new WaitForSeconds(healDelay);
        while (currentHealth < startHealth)
        {
            currentHealth += healRate * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
