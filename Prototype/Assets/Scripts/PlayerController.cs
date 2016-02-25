using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float health = 50;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

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
