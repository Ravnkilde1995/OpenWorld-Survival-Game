using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float hungerEffect;
    public float thirstEffect;

    // --- Equipping --- //
    public bool isEquippable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;

    public bool isSelected;


    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;

        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName")?.GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription")?.GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality")?.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, hungerEffect, thirstEffect);
            }

            if (isEquippable && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }
        }
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.eating);
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculeList();
                //CraftingSystem.Instance.RefreshNeededItems();
                Debug.Log("Comsumed item.. added some health and food");
            }
        }
    }

   private void consumingFunction(float healthEffect, float hungerEffect, float thirstEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        hungerEffectCalculation(hungerEffect);

        thirstEffectCalculation(thirstEffect);

    } 


   private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //

        float healthBeforeConsumption = PlayerStats.Instance.currentHealth;
        float maxHealth = PlayerStats.Instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerStats.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerStats.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


   private static void hungerEffectCalculation(float hungerEffect)
    {
        // --- Calories --- //

        float hungerBeforeConsumption = PlayerStats.Instance.currentHunger;
        float maxCalories = PlayerStats.Instance.maxHunger;

        if (hungerEffect != 0)
        {
            if ((hungerBeforeConsumption + hungerEffect) > maxCalories)
            {
                PlayerStats.Instance.setHunger(maxCalories);
            }
            else
            {
                PlayerStats.Instance.setHunger(hungerBeforeConsumption + hungerEffect);
            }
        }
    }


   private static void thirstEffectCalculation(float thirstEffect)
    {
        // --- Hydration --- //

        float thirstBeforeConsumption = PlayerStats.Instance.currentThirst;
        float maxHydration = PlayerStats.Instance.maxThirst;

        if (thirstEffect != 0)
        {
            if ((thirstBeforeConsumption + thirstEffect) > maxHydration)
            {
                PlayerStats.Instance.setThirst(maxHydration);
            }
            else
            {
                PlayerStats.Instance.setThirst(thirstBeforeConsumption + thirstEffect);
            }
        }
    }


}