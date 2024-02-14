using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CURRENTLY UNUSED
// to play the background audio during the game
public class BackgroundSound : MonoBehaviour
{

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null) {
            audioSource.loop = true;
            audioSource.Play();
        } else {
            Debug.LogError("Cannot find audio source");
        }
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
