using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float aggroRange = 20f;
    [SerializeField] private float attackRange = 5f;

    private NavMeshAgent navMeshAgent;
    private GameObject player;  // Reference to the player GameObject
    bool CheckForDamage;

    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");  // Find the player by tag
    }

    void Update()
    {
        if (player != null)  // Check if player reference is not null
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < aggroRange)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.enemyChase);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.transform.position);

                animator.SetBool("Running", true);
            }
            else
            {
                    SoundManager.Instance.enemyChase.Stop();
                    navMeshAgent.isStopped = true;

                animator.SetBool("Running", false);
            }

            if(distanceToPlayer <= attackRange)
            {

                // Stop the troll from running
                animator.SetBool("Running", false);

                //Attack
                animator.SetTrigger("Attack");
            }
        }
    }

    IEnumerator PlayGotHitWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.Instance.PlaySound(SoundManager.Instance.gotHit);
    }

    public void GetHit()
    {
       

        // play sound when troll is hitting player
        SoundManager.Instance.PlaySound(SoundManager.Instance.bigHit);
        
        // play sound when gotHit is called with a delay
        StartCoroutine(PlayGotHitWithDelay());

        //animator.SetTrigger("shake");

        // subtrack 50 points from currentHealth
        PlayerStats.Instance.currentHealth -= 50;

        // Check if current health is under 0 
        if (PlayerStats.Instance.currentHealth <= 0)
        {
            //SoundManager.Instance.PlaySound(SoundManager.Instance.playerDeath);
            //respawn();
            Debug.Log("You died");

        }
    }
}