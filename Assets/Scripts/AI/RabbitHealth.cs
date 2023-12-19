using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHealth : MonoBehaviour
{
    public bool playerInRange;
    public float maxHealth = 100f;
    public float currentHealth;

    public RabbitHealthBar healthBar;

    [Header("Sounds")]
    [SerializeField] AudioSource soundChannel;
    [SerializeField] AudioClip rabbitHitSound;
    [SerializeField] AudioClip rabbitDeathSound;

    private Animator animator;
    public bool isDead;

    [SerializeField] ParticleSystem blood;

    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead == false)
        {
            currentHealth -= damage;

            blood.Play();
            //soundChannel.PlayOneShot(rabbitHitSound);
            healthBar.SetHealth(currentHealth);
 
            if (currentHealth <= 0)
            {
                soundChannel.PlayOneShot(rabbitDeathSound);
 
                animator.SetTrigger("DIE");
                GetComponent<AI_Movement>().enabled = false;

                isDead = true;
            }
            else
            {
            soundChannel.PlayOneShot(rabbitHitSound);
            }
        }
    
    }

    private void PlayDyingSound()
    {
        soundChannel.PlayOneShot(rabbitDeathSound);
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
