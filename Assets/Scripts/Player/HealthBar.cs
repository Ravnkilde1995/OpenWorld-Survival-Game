using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthCount;
    public GameObject playerStats;

    private float currentHealth, maxHealth;


    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerStats.GetComponent<PlayerStats>().currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }   
        maxHealth = playerStats.GetComponent<PlayerStats>().maxHealth;
        float fill = currentHealth / maxHealth;
        slider.value = fill;
        healthCount.text = currentHealth.ToString() + "/" + maxHealth.ToString();        
    }
}
