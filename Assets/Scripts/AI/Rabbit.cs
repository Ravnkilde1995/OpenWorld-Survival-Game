using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    //public string name = "Rabbit";
    public bool playerInRange;
    public RabbitHealthBar healthBar;

    public float currentHealth;
    public float maxHealth;

    [Header("Sounds")]
    [SerializeField] AudioSource soundChannel;
    [SerializeField] AudioClip rabbitHitSound;
    [SerializeField] AudioClip rabbitDeathSound;

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        soundChannel.PlayOneShot(rabbitHitSound);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            soundChannel.PlayOneShot(rabbitDeathSound);
            Destroy(gameObject);

        }
        else
        {
            soundChannel.PlayOneShot(rabbitHitSound);
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Spilleren er i range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spilleren er ikke i range");
            playerInRange = false;
        }
    }
}
