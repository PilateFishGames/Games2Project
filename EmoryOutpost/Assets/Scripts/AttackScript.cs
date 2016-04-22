using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    ArmoredController parent;

	// Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<ArmoredController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parent.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            parent.playerInRange = false;
        }
    }
}
