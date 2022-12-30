using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectKey : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Enter key is pressed");
            }
            if (Input.GetKey(KeyCode.Return))
            {
                Debug.Log("Enter key is continuously pressed");
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Enter key is released");
            }

        }
    }
}



public class keypress1 : MonoBehaviour
{


}
