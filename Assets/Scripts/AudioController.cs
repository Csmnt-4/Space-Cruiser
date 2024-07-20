using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    bool paused = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
    }

    // Update is called once per frame
    public void PlaySwitch()
    {
        if (audioSource != null)
        {
            if (!paused)
            {
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = 1;
            }

            paused = !paused;
        }
    }
}
