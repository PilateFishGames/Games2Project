using UnityEngine;
using System.Collections;

public class Cocoon : MonoBehaviour {

    public AudioClip[] clips;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        int index = Random.Range(0, clips.Length);
        source = GetComponent<AudioSource>();
        source.clip = clips[index];
        source.Play();
	}
}
