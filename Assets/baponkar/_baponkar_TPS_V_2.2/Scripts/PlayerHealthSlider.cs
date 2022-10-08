using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    public Slider healthSlider;
    public Health health;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = Mathf.Clamp(health.currentHealth, 0, health.maxHealth);
    }
}
