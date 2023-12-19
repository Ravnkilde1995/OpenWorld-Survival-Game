using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EquipAbleItem : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InventorySystem.Instance.isOpen == false && PauseMenu.isPaused == false
            && CraftingSystem.Instance.isOpen == false) // left mouse btn && inventory, pausemenu and crafting is not open
        {
            //Deplay the sound by 0.2 sec
            StartCoroutine(SwingSoundDelay());

            animator.SetTrigger("hit"); // our trigger parameter in animator
        }
    }

    public void GetHit()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;

        if (selectedTree != null)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.chopSound);
            selectedTree.GetComponent<ChopableTree>().GetHit();
        }
    }

    IEnumerator SwingSoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        // Audio for swinging item in inventory
        SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwing);
    }
}
