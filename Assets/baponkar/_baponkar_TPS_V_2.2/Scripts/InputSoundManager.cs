using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.PlayOneShot(audioClips[1]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioSource.PlayOneShot(audioClips[2]);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
    }
}
