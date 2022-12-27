using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootStepSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] footStepSounds;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    
}
