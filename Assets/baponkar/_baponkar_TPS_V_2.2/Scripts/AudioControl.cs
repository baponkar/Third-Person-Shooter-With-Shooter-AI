using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip land;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLand()
    {
        audioSource.PlayOneShot(land);
    }
}
