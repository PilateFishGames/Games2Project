using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    public float speed;

    public Transform endPoint;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
    }
}
