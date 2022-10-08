using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifeTime = 1.0f;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
       if(time > 0)
        {
            time -= Time.deltaTime;
        }
       else
        {
            Destroy(gameObject);
        }
    }
}
