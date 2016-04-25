using UnityEngine;
using System.Collections;

public class CameraControlScript : MonoBehaviour {
    public Camera[] cameras;
    private int activeCameraIndex;


	// Use this for initialization
	void Start () {
        activeCameraIndex = 0;

        //Turn all cameras off, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        cameras[0].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (activeCameraIndex != 0)
            {
                cameras[0].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (activeCameraIndex != 1)
            {
                cameras[1].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeCameraIndex != 2)
            {
                cameras[2].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (activeCameraIndex != 3)
            {
                cameras[3].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (activeCameraIndex != 4)
            {
                cameras[4].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (activeCameraIndex != 5)
            {
                cameras[5].gameObject.SetActive(true);
                cameras[activeCameraIndex].gameObject.SetActive(false);
                activeCameraIndex = 5;
            }
        }
    }
}
