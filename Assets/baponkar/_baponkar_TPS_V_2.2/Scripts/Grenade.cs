using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timer = 3f;
    float countdown;
    bool isExploded = false;

    public float blastRadius = 5f;
    public float blastForce = 1000f;

    public GameObject explosionEffect;

    public AudioSource audioSource;
    public AudioClip blastSound;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0.0f && !isExploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        //sound
        if(audioSource && blastSound)
        {
            audioSource.PlayOneShot(blastSound);
        }
       //effect
        explosionEffect.SetActive(true);
        explosionEffect.GetComponent<ParticleSystem>().Play();
        

       //find near object
       Collider [] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);
        //add Foce to them
        foreach(Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }

            if(nearbyObject.GetComponent<Destructible>() != null)
            {
                nearbyObject.GetComponent<Destructible>().Destruction();
            }

        }

        //give force to the destroyed object
        Collider [] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider nearbyObject in collidersToMove)
        {
            if(nearbyObject.GetComponent<Rigidbody>() != null)
            {
                nearbyObject.GetComponent<Rigidbody>().AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }
        //Damage

        //Destroy
        Destroy(gameObject);
        isExploded = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // if(collision.gameObject.tag == "Grenade")
        // {
        //     Explode();
        // }
    }
}
