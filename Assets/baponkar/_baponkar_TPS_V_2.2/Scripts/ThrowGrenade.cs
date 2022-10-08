using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ThrowGrenade : MonoBehaviour
{
    public Transform Hand;
    public float throwForce;
    public GameObject grenadPrefab;
    public KeyCode key;

    public Animator anim;
    bool isThrowing;
    GameObject grenade;
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !isThrowing)
        {
            StartCoroutine(Throw());
            isThrowing = false;
        }
    }
    IEnumerator Throw()
    {
        isThrowing = true;
        grenade = Instantiate(grenadPrefab, Hand.position, Hand.rotation);
        grenade.transform.parent = Hand;
        grenade.transform.localPosition = new Vector3(0, 0, 0);
        grenade.transform.localRotation = Quaternion.identity;
        anim.SetTrigger("isThrowingGrenade");
        yield return new WaitForSeconds(3.2f);
    }

    public void ThrowGrenadeNow()
    {
        grenade.transform.parent = null;
        Rigidbody rb = grenade.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        grenade.GetComponent<Grenade>().enabled = true;
        rb.AddForce(transform.forward * throwForce,ForceMode.Impulse);
    }
}
