using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{

    private AudioSource audioSource;
    // Start is called before the first frame update
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
    void Update()
    {
        
    }
}
