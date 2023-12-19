using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Thirstbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI thirstCount;
    public GameObject playerStats;

    private float currentThirst, maxThirst;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentThirst = playerStats.GetComponent<PlayerStats>().currentThirst;

        if (currentThirst <= 0)
        {
            currentThirst = 0;
        }
        maxThirst = playerStats.GetComponent<PlayerStats>().maxThirst;
        float fill = currentThirst / maxThirst;
        slider.value = fill;
        thirstCount.text = currentThirst.ToString() + "/" + maxThirst.ToString();        
    }
}