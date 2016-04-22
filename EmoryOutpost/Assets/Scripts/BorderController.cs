using UnityEngine;

public class BorderController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        HorrorController.followPlayer = !HorrorController.followPlayer;
        HorrorController.playFollow = true;
    }
}
