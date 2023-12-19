/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerIsDamaged(int damage)
    {
        // Debug log to check if PlayerStats.Instance is null
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats.Instance is null. Check initialization.");
            return;
        }

        // play sound when gotHit is called with a delay
        StartCoroutine(PlayerTookDamageDelay());

        // subtrack damage points from currentHealth
        PlayerStats.Instance.currentHealth -= damage;

        // Check if current health is under 0 
        if (PlayerStats.Instance.currentHealth < 0)
        {
            PlayerIsDead();
        }
    }

    public void PlayerIsDead()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.playerDeath);
        Debug.Log("You died");
    }

    IEnumerator PlayerTookDamageDelay()
    {
        yield return new WaitForSeconds(0.2f);
        //calling the playerisdamged method with 50 damage
        SoundManager.Instance.PlaySound(SoundManager.Instance.gotHit);
    }

}
*/