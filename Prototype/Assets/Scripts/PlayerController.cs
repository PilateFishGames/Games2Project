using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Camera cam;

	public LayerMask lm;

	public float damage = 10.0f;
	public float range = 100.0f;

	public float shotDelay;
	public float timeLeft;

	public bool isAuto = false;

	public float health = 50;

	// Use this for initialization
	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;
		if (cam == null) {
			Debug.LogError ("Camera is null");
			this.enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (isAuto) {
			if (Input.GetButton ("Fire1") && timeLeft <= 0) {
				Shoot ();
				timeLeft = shotDelay;
			}
			timeLeft -= Time.deltaTime;
		} 
		else {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		}
		if (Input.GetKeyDown ("left alt")) {
			isAuto = !isAuto;
		}
	}

	private void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hitInfo, range, lm)) {
			Debug.Log (hitInfo.collider.name + " was hit");
			hitInfo.collider.gameObject.GetComponent<Enemy>().dealDamage (damage);
		}
	}

	public void dealDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Debug.Log ("You are dead!");
		} 
		else {
			Debug.Log("Health: " + health);
		}
	}
}
