using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 20.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	
	float verticalVelocity = 0;
	
	CharacterController characterController;

	public float startHealth = 50;
	public float currentHealth;

	// Use this for initialization
	void Start () {
		characterController = gameObject.GetComponent<CharacterController>();
		GameController.followMouse = true;
		currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		if (GameController.followMouse) {
			transform.Rotate (0, rotLeftRight, 0);
		}

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		if (GameController.followMouse) {
			Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);	
		}
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if( characterController.isGrounded && Input.GetButton("Jump") ) {
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );
		
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );
	}

	public void takeDamage(float damage) {
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Debug.Log ("You are dead!");
		} 
		else {
			Debug.Log("Health: " + currentHealth);
		}
	}
}
