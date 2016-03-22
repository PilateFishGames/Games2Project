using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Camera cam;
	
	public LayerMask lm;
	
	public float damage = 10.0f;
	public float range = 100.0f;
	
	public float shotDelay;
	public float timeLeft;
	
	public bool isAuto;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			Debug.LogError ("Camera is null");
			this.enabled = true;
		}

		isAuto = true;
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
	}

	private void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hitInfo, range, lm)) {
			Debug.Log (hitInfo.collider.name + " was hit");
			hitInfo.collider.gameObject.GetComponent<EnemyController>().dealDamage (damage);
		}
	}
}
