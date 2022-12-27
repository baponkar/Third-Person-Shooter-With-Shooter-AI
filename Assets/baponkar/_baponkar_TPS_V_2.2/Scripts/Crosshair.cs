using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RectTransform reticle;

    public float restSize, moveSize;
    public float speed;
    public float currentSize;

    public bool isMoving
    {
        get 
        {
            if(Input.GetAxis("Horizontal") != 0 ||
               Input.GetAxis("Vertical") != 0 ||
               Input.GetAxis("Mouse X") != 0 ||
               Input.GetAxis("Mouse Y") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    void Start()
    {
        currentSize = restSize;
        reticle = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        if(isMoving)
        {
            currentSize = Mathf.Lerp(currentSize, moveSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }
}
