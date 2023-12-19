using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.instance.onTarget && SelectionManager.instance.selectedObject == gameObject)
        {
            if (!InventorySystem.Instance.CheckIfFull()) {
                InventorySystem.Instance.AddToInventory(ItemName);
                Debug.Log("added to inventory");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
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