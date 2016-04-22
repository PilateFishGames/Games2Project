using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {

	public GameObject explosionEffect;
	public Transform  explosionEffectLocation;
	public float health;

	private bool bExploded;
	private GameEffectsController gameEffectsController;

	void Awake()
	{
		bExploded = false;

		GameObject goTemp = GameObject.FindGameObjectWithTag ("GameController");
		gameEffectsController = goTemp.GetComponent<GameEffectsController> ();
	}

	void Update () 
	{
	
		if (health <= 0 && bExploded == false )
		{
			bExploded = true;
			Instantiate (explosionEffect, explosionEffectLocation.position, Quaternion.LookRotation( Vector3.up ) );
			Destroy (gameObject );

			gameEffectsController.BarrelDestroyed();
		}
	}



	public void TakeDamage( float flDamage )
	{
		health -= flDamage;
	}
}
