using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitHealthBar : MonoBehaviour
{
     public Slider slider;

     private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

}
