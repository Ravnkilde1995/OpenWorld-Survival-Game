using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject ItemInfoUI;

    public static InventorySystem Instance { get; set; }
    
    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList= new List<string>();

    private GameObject whatSlotToEquip;
    private GameObject itemToAdd;
    //public bool isFull;
    public bool isOpen;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Equipping
    public bool isEquipable;
    private GameObject itemPendingEquipping;
    public bool isNowEquipped;

    //Pickup popup
    /*public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;*/
    


    void Start()
    {
        isOpen = false;

        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen && !PauseMenu.isPaused)
        {

            //Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            isOpen = true;

            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;
            

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;

            SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
        }
        if (PauseMenu.isPaused && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        // adding pickup item to inventory sound
        SoundManager.Instance.PlaySound(SoundManager.Instance.pickup);

        whatSlotToEquip = findNextEmptySlot();

        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);

        ReCalculeList();
        
        if (CraftingSystem.Instance != null)
        {
            CraftingSystem.Instance.RefreshNeededItems();
        }

    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter!=0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                }
                
            }
        }
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void ReCalculeList()
    {
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0 )
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, ""); 

                itemList.Add(result);
            }
        }
    }

    private GameObject findNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }
        if (counter == 28)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}