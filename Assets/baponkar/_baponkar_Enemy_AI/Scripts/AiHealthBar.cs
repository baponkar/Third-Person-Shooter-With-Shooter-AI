using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ThirdPersonShooter.Ai
{
public class AiHealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [HideInInspector] public Health health;

    [HideInInspector] public Slider slider;
    
    void Start()
    {
        health = GetComponentInParent<Health>();
        slider = GetComponentInChildren<Slider>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(!health.isDead)
        {
            transform.position =Camera.main.WorldToScreenPoint(target.position + offset);
            slider.value = health.currentHealth / health.maxHealth;
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
}
}