using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hungerbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hungerCount;
    public GameObject playerStats;

    private float currentHunger, maxHunger;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHunger = playerStats.GetComponent<PlayerStats>().currentHunger;

        if (currentHunger <= 0)
        {
            currentHunger = 0;
        }
        maxHunger = playerStats.GetComponent<PlayerStats>().maxHunger;
        float fill = currentHunger / maxHunger;
        slider.value = fill;
        hungerCount.text = currentHunger.ToString() + "/" + maxHunger.ToString();        
    }
}
