using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour {

    public float reloadAmount;
    private GunController gun;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        gun = FindObjectOfType<GunController>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(gun.ammo > 0)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
	}

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            gun.ammo = reloadAmount;
            source.Play();
        }
    }
}
