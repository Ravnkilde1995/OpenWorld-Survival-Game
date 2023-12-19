using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }
    public GameObject player;


    //Health//
    public float currentHealth;
    public float maxHealth;
    public float minHealth = 0f;

    //Hunger//
    public float currentHunger;
    public float maxHunger;
    public float minHunger = 0f;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    //Thirst//
    public float currentThirst;
    public float maxThirst;
    public float minThirst = 0f;

    public float healthDecreaseRate = 1;
    float nextDecreaseTime;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        
        StartCoroutine(Thirst());
    }

    IEnumerator Thirst()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            currentThirst -= 1;
        }
    }
   

    private void Update()
    {
        distanceTravelled += Vector3.Distance(player.transform.position, lastPosition);
        lastPosition = player.transform.position;

        if (distanceTravelled >= 15)
        {
            currentHunger -= 1;
            distanceTravelled = 0;
        }

        if (currentHunger <= 0f && currentThirst <= 0f)
        {
            if (Time.time >= nextDecreaseTime)
            {
                Damage(healthDecreaseRate * Time.deltaTime);

                nextDecreaseTime = Time.time + 1f;
            }
        }
        else
        {
            // Reset the next decrease time when hunger or thirst is not at 0
            nextDecreaseTime = 0f;
        }

    }

    private void Damage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Max(0f, currentHealth);
    }

    public void EatFood(float food)
    {
        currentHunger += food;

        currentHunger = Mathf.Clamp(currentHunger, minHunger, maxHunger);
    }

    public void DrinkWater(float water)
    {
        currentThirst += water;

        currentThirst = Mathf.Clamp(currentThirst, minThirst, maxThirst);
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    public void setHunger(float newHunger)
    {
        currentHunger = newHunger;
    }

    public void setThirst(float newThirst)
    {
        currentThirst = newThirst;
    }
}
