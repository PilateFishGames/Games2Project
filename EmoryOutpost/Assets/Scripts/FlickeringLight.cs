using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

    public float minTime = 0.05f;
    public float maxTime = 1.2f;

    private float timer;

    private Light fl;

    private MeshRenderer mr;

    public Material mat1;
    public Material mat2;

	// Use this for initialization
	void Start () {
        fl = GetComponent<Light>();
        mr = GetComponent<MeshRenderer>();
        timer = Random.Range(minTime, maxTime);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            fl.enabled = !fl.enabled;
            if (fl.enabled)
            {
                mr.material = mat1;
            }
            else
            {
                mr.material = mat2;
            }
            timer = Random.Range(minTime, maxTime);
        }
	}
}
