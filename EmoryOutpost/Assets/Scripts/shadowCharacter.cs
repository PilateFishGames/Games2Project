using UnityEngine;
using System.Collections;

public class shadowCharacter : MonoBehaviour {
    public Transform target;
    public float speed; 
    private Animation anim;
    private bool onTarget;

  
   // public float attackTime;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        onTarget = false;
	}
   
	// Update is called once per frame
	void Update () {
        if (!onTarget)
        {
            anim.Play("walk");  
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
      
        
 
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "holdPosition")
        {
            onTarget = true;
            Debug.Log("Trigger entered");
            anim.Play("attack");
            
        }
       // if (other.tag == "nextPostion")
     //   {
     //       onTarget = true;
     //       Debug.Log("Tringger entered");
     //       anim.Play("attack");
     //   }
       

            }
}
