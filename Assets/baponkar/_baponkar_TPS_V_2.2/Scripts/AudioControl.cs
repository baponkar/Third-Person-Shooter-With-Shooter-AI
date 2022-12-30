using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip land;
    public int groundIndex;
    public AudioClip[] grassStep,gravelStep,metalStep,normalStep;

    public Transform groundCheck, groundCheck_1;
    RaycastHit hit;
    public float range = 1f;
    public LayerMask layer;

    public AudioSource gruntEffect;
    public Health health;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        groundIndex = GetGroundIndex();
        if(health.getShot && !health.isDead)
        {
            PlayGrunt();
        }
        
    }


    int GetGroundIndex()
    {
        int index = 0;
        Vector3 direction = (groundCheck_1.position - groundCheck.position).normalized;
        if (Physics.Raycast(groundCheck.position, direction, out hit, range, layer))
            index = hit.transform.gameObject.layer;
            
        return index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck_1.position);
    }

    public void PlayLand()
    {
        audioSource.PlayOneShot(land);
    }

    public void FootStep()
    {
        switch(groundIndex)
        {
            case 14:
                PlayStep(grassStep);
                break;
            case 15:
                PlayStep(gravelStep);
                break;
            case 16:
                PlayStep(metalStep);
                break;
            default:
                PlayStep(normalStep);
                break;
        }
    }

    void PlayStep(AudioClip[] clips)
    {
        int randomIndex = (int)Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[randomIndex]);
    }

    void PlayGrunt()
    {
        if(!gruntEffect.isPlaying)
        {
            gruntEffect.PlayOneShot(gruntEffect.clip);
        }
    }
}
