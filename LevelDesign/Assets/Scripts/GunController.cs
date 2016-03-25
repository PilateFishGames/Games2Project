using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Camera cam;
	
	public LayerMask lm;
	
	public float damage = 10.0f;
	public float range;
	
	public float shotDelay;
	public float timeLeft;
	
	public bool isAuto;

	private Vector3 defaultPosition;
	private Vector3 recoilSpeed;
	public float recoilAngle;
	private float recoilAngleSpeed;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			Debug.LogError ("Camera is null");
			this.enabled = true;
		}
		defaultPosition = transform.localPosition;
		recoilAngle = 0;
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
		transform.localPosition = Vector3.SmoothDamp (transform.localPosition, defaultPosition, ref recoilSpeed, 0.1f);
		//recoilAngle = Mathf.SmoothDamp (recoilAngle, 0, ref recoilAngleSpeed, 0.1f);
		//transform.localEulerAngles += Vector3.left * recoilAngle;
	}

	private void Aim(Vector3 aimPoint) {
		
	}

	private void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hitInfo, range, lm)) {
			//Debug.Log (hitInfo.collider.name + " was hit");
			hitInfo.collider.gameObject.GetComponent<EnemyController>().takeDamage (damage);
		}
		transform.localPosition -= Vector3.forward * 0.02f;
		//recoilAngle += 5.0f;
		//Mathf.Clamp (recoilAngle, 0, 30);
		//transform.localEulerAngles += Vector3.left * recoilAngle;
	}
}
