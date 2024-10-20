using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] dialogueSounds = new AudioClip[27];

    int index;

    public void PlayCharSound(char c)        //plays sound of a char
    {
        index = char.ToUpper(c) - 65;
        if (index < 0 || index > 26) { index = 26; }

        audioSource.clip = dialogueSounds[index];
        audioSource.Play();
    }
}
