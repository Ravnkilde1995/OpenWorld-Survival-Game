using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject ToolHolder;
    public GameObject selectedItemModel;

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


    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }

    }

    void SelectQuickSlot(int number)
    {
        if (checkIfSlotIsFull(number))
        {
            // Unselect previously selected item if the same key is pressed
            if (selectedItem != null)
            {
                if (selectedNumber == number)
                {
                    InventoryItem inventoryItem = selectedItem.gameObject.GetComponent<InventoryItem>();
                    if (inventoryItem != null)
                    {

                        inventoryItem.isSelected = false;
                        UnequipItem(number);
                        return;
                    }
                }
            }

            // Unselect previously selected item
            UnequipItem(selectedNumber);

            // Set the new selected number
            selectedNumber = number;

            // Set the new selected item
            selectedItem = GetSelectedItem(number);

            InventoryItem selectedItemComponent = selectedItem.GetComponent<InventoryItem>();
            if (selectedItemComponent != null)
            {
                selectedItemComponent.isSelected = true;

                EquipItem(number);
            }
        }
    }

    private void EquipItem(int number)
    {
        SetEquippedModel(selectedItem);

        // Changing color
        for (int i = 0; i < numbersHolder.transform.childCount; i++)
        {
            Transform child = numbersHolder.transform.GetChild(i);
            Transform textTransform = child.Find("Text (TMP)");

            if (child != null && child.name == "number" + number)
            {
                if (textTransform != null)
                {
                    TextMeshProUGUI toBeChanged = child.GetComponentInChildren<TextMeshProUGUI>();
                    if (toBeChanged != null)
                    {
                        toBeChanged.color = Color.green;
                    }
                }
            }
        }
    }

    private void UnequipItem(int number)
    {
        // Unselect previously selected item 
        if (selectedItem != null)
        {
            InventoryItem inventoryItem = selectedItem.gameObject.GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                inventoryItem.isSelected = false;
            }

            if (selectedItemModel != null)
            {
                Destroy(selectedItemModel.gameObject);
                selectedItemModel = null;

                // Reset the selected number to allow for re-equip
                selectedNumber = -1;
            }


            // Changing color
            for (int i = 0; i < numbersHolder.transform.childCount; i++)
            {
                Transform child = numbersHolder.transform.GetChild(i);
                Transform textTransform = child.Find("Text (TMP)");

                if (child != null && child.name == "number" + number)
                {
                    if (textTransform != null)
                    {
                        TextMeshProUGUI toBeChanged = textTransform.GetComponent<TextMeshProUGUI>();
                        if (toBeChanged != null)
                        {
                            toBeChanged.color = Color.white;
                        }
                    }
                }
            }
        }
    }

    private void SetEquippedModel(GameObject selectedItem)
    {
        // if we already have a selected item then destroy.
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
            Debug.Log("destroyed axe");
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
         selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            //placement of tool & rotation of tool
            new Vector3(0.2f, -0.3f, 0.3f), Quaternion.Euler(-20f, 180f, 0f));
        selectedItemModel.transform.SetParent(ToolHolder.transform, false);
    }

    internal int GetWeaponDamage()
    {

        if (selectedItem != null)
        {
            return selectedItem.GetComponent<Weapon>().weaponDamage;
        }
        else
        {
            return 0;
        }
    }

    internal bool IsHoldingWeapon()
    {
        if (selectedItem != null)
        {
            if (selectedItem.GetComponent<Weapon>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    bool checkIfSlotIsFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber-1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("(Clone)", "");
        // Adding item to list
        itemList.Add(cleanName);

        InventorySystem.Instance.ReCalculeList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
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

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    
}