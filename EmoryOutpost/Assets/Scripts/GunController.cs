using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public float damage = 10.0f;
    public float range;
    public float shotDelay;
    public float timeLeft;
    public bool isAuto;
    //public float recoilAngle;
    public float ammo;

    //private float recoilAngleSpeed;

    public Camera cam;
	public LayerMask lm;
    public ParticleSystem muzzleFlash;
    public Transform barrelPoint;
    public AudioClip fireClip;
    public AudioClip outClip;

    private Vector3 defaultPosition;
	private Vector3 recoilSpeed;
    private AudioSource source;
    private EnemyMonster monster;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			Debug.LogError ("Camera is null");
			this.enabled = true;
		}
		defaultPosition = transform.localPosition;
		//recoilAngle = 0;
        source = GetComponent<AudioSource>();
        monster = FindObjectOfType<EnemyMonster>();
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
	}

	private void Shoot() {
        if(!PauseMenu.isPaused)
        {
            if (ammo > 0)
            {
                source.clip = fireClip;
                source.Play();
                RaycastHit hitInfo;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range, lm))
                {
                    switch (hitInfo.collider.gameObject.tag)
                    {
                        case "Enemy":
                            hitInfo.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage, hitInfo.point);
                            break;
                        case "Armored":
                            hitInfo.collider.gameObject.transform.parent.GetComponent<ArmoredController>().takeDamage(damage, hitInfo.point);
                            break;
                        case "Baby":
                            hitInfo.collider.gameObject.transform.parent.GetComponent<BabyController>().takeDamage(damage, hitInfo.point);
                            break;
                        case "Flower":
                            monster.takeNotice();
                            break;
                        default:
                            break;
                    }
                }
                Instantiate(muzzleFlash, barrelPoint.position, barrelPoint.rotation);
                transform.localPosition -= Vector3.forward * 0.02f;
                ammo--;
            }
            else
            {
                source.clip = outClip;
                source.Play();
            }
        }
    }
}
