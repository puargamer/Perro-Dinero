using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LittleGuyAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) { audioSource.Play(); }     //use this line while little guy is walking
    }

    public void playWalkSound()
    {
        audioSource.clip = walkSound;
        audioSource.pitch = Random.Range(-1,1);
        audioSource.Play();
        audioSource.pitch = 1f;
    }
}
