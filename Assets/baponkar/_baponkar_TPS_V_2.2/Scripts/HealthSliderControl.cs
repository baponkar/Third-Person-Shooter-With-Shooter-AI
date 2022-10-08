using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSliderControl : MonoBehaviour
{
    public Slider slider;
    public Health health;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.currentHealth;
    }
}
