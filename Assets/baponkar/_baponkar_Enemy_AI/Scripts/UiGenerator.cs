using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ThirdPersonShooter.Ai
{
public class UiGenerator : MonoBehaviour
{
    public GameObject uiPrefab;
    AiHealthBar healthBar;
    WeaponWidget widget;
    
    // Start is called before the first frame update
    void Start()
    {
        widget = GetComponent<WeaponWidget>();
        var ui = Instantiate(uiPrefab);
        ui.transform.SetParent(transform,false);
        healthBar = ui.GetComponentInChildren<AiHealthBar>();

       
        
        if(healthBar != null)
        {
            healthBar.target = transform;
        }
        
        //widget.text = ui.GetComponentsInChildren<Text>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}