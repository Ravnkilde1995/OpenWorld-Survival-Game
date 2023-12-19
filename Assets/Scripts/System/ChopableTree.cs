using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChopableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float TreeMaxHealth;
    public float TreeHealth;

    public float caloriesSpentChopping = 3;
    public float thirstdecreasedChopping = 5;

    public Animator animator;

    public void Start()
    {
        TreeHealth = TreeMaxHealth;
        StartCoroutine(UpdateHealthContinuously());
        animator = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = TreeHealth;
            GlobalState.Instance.resourceMaxHealth = TreeMaxHealth;
        }
    }

    private IEnumerator UpdateHealthContinuously()
    {
        while (true)
        {
            GlobalState.Instance.resourceHealth = TreeHealth;
            GlobalState.Instance.resourceMaxHealth = TreeMaxHealth;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void GetHit()
    {
        animator.SetTrigger("shake");

        TreeHealth -= 1;

        PlayerStats.Instance.currentHunger -= caloriesSpentChopping;
        PlayerStats.Instance.currentThirst -= thirstdecreasedChopping;

        if (TreeHealth < 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.ObjectDestroyed);
            TreeIsDead();
        }
    }

    public IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.6f);

    }

    void TreeIsDead()
    {
        // get position of tree
        Vector3 treePosition = transform.position;
        //Destroy the object
        Destroy(transform.parent.gameObject);
        //Cannot already chop a chopped tree s� set false & set ui to false too
        canBeChopped = false;
        SelectionManager.instance.selectedTree = null;
        SelectionManager.instance.chopHolder.gameObject.SetActive(false);
        //Instantiate new object (Prefab) from Resource folder 
        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"),
            treePosition, Quaternion.Euler(0, 0, 0));
    }
}