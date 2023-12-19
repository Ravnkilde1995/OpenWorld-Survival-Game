using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public GameObject weaponsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    Button toolsBTN;
    Button weaponsBTN;
    Button closeBTN;
    Button backBTNWeapon;
    Button backBTNTool;

    //Craft Buttons
    Button craftAxeBTN;
    Button craftSpearBTN;
    Button craftTorchBTN;
    Button craftPickaxeBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;
    TextMeshProUGUI TorchReq1, TorchReq2;
    TextMeshProUGUI SpearReq1, SpearReq2;
    TextMeshProUGUI PickaxeReq1, PickaxeReq2;

    public bool isOpen;

    //All Blueprints
    public BluePrint AxeBluePrint = new BluePrint("Axe", 2, "Stone", 3, "Wood", 3);
    public BluePrint SpearBluePrint = new BluePrint("Spear", 2, "Stone", 1, "Wood", 5);
    public BluePrint PickaxeBluePrint = new BluePrint("Pickaxe", 2, "Stone", 5, "Wood", 3);
    public BluePrint TorchBluePrint = new BluePrint("Torch", 2, "Stone", 1, "Wood", 2);

    public static CraftingSystem Instance { get; set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        
        weaponsBTN = craftingScreenUI.transform.Find("WeaponsButton").GetComponent<Button>();
        weaponsBTN.onClick.AddListener(delegate { OpenWeaponsCategory(); });
        
        closeBTN = craftingScreenUI.transform.Find("Close").GetComponent<Button>();
        closeBTN.onClick.AddListener(delegate { craftingScreenUI.gameObject.SetActive(false); isOpen = false; });
        
        backBTNTool = toolsScreenUI.transform.Find("Back").GetComponent<Button>();
        backBTNTool.onClick.AddListener(delegate { GobackTools(); });
       
        backBTNWeapon = weaponsScreenUI.transform.Find("Back").GetComponent<Button>();
        backBTNWeapon.onClick.AddListener(delegate { GobackWeapons(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();
        
        //Spear
        SpearReq1 = weaponsScreenUI.transform.Find("Spear").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        SpearReq2 = weaponsScreenUI.transform.Find("Spear").transform.Find("req2").GetComponent<TextMeshProUGUI>();
        
        //Pickaxe
        PickaxeReq1 = toolsScreenUI.transform.Find("Pickaxe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        PickaxeReq2 = toolsScreenUI.transform.Find("Pickaxe").transform.Find("req2").GetComponent<TextMeshProUGUI>();
        
        //Torch
        TorchReq1 = toolsScreenUI.transform.Find("Torch").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        TorchReq2 = toolsScreenUI.transform.Find("Torch").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        //Axe Craft
        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBluePrint); });

        //Spear Craft
        craftSpearBTN = weaponsScreenUI.transform.Find("Spear").transform.Find("Button").GetComponent<Button>();
        craftSpearBTN.onClick.AddListener(delegate { CraftAnyItem(SpearBluePrint); });

        //Pickaxe Craft
        craftPickaxeBTN = toolsScreenUI.transform.Find("Pickaxe").transform.Find("Button").GetComponent<Button>();
        craftPickaxeBTN.onClick.AddListener(delegate { CraftAnyItem(PickaxeBluePrint); });

        //Torch Craft
        craftTorchBTN = toolsScreenUI.transform.Find("Torch").transform.Find("Button").GetComponent<Button>();
        craftTorchBTN.onClick.AddListener(delegate { CraftAnyItem(TorchBluePrint); });
    }

    private void CraftAnyItem(BluePrint bluePrint)
    {
        if (InventorySystem.Instance != null)
        {
            Debug.Log($"Crafting {bluePrint.itemName}");

            // add item into inventory
            SoundManager.Instance.PlaySound(SoundManager.Instance.crafting);
            InventorySystem.Instance.AddToInventory(bluePrint.itemName);

            // check sum of requirements
            // can add more if needed.

            if (bluePrint.numOfRequirements == 1)
            {
                InventorySystem.Instance.RemoveItem(bluePrint.Req1, bluePrint.Req1amount);
            }
            else if (bluePrint.numOfRequirements == 2)
            {
                InventorySystem.Instance.RemoveItem(bluePrint.Req1, bluePrint.Req1amount);
                InventorySystem.Instance.RemoveItem(bluePrint.Req2, bluePrint.Req2amount);
            }

            InventorySystem.Instance.ReCalculeList();

            StartCoroutine(Calculate());
        }
        else
        {
            Debug.LogError("InventorySystem.Instance is null. Ensure that InventorySystem is properly initialized.");
        }
    }

    public IEnumerator Calculate()
    {
        yield return 0; // No delay
        InventorySystem.Instance.ReCalculeList();
        RefreshNeededItems();
    }


    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void GobackTools()
    {
        craftingScreenUI.SetActive(true);
        toolsScreenUI.SetActive(false);
    }

    void GobackWeapons()
    {
        craftingScreenUI.SetActive(true);
        weaponsScreenUI.SetActive(false);
    }
    void OpenWeaponsCategory()
    {
        craftingScreenUI.SetActive(false);
        weaponsScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
        
            craftingScreenUI.SetActive(true);
            //Curser.lockstate = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            weaponsScreenUI.SetActive(false);
            //Curser.lockstate = CursorLockMode.Locked;
            isOpen = false;
        }

    }

    public void RefreshNeededItems()
    {

        int stone_count = 0;
        int wood_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch(itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Wood":
                    wood_count += 1;
                    break;
            }
        }

        // Axe 

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Wood [" + wood_count + "]";

        if (stone_count >= 3 && wood_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }

        // Torch 

        TorchReq1.text = "1 Stone [" + stone_count + "]";
        TorchReq2.text = "2 Wood [" + wood_count + "]";

        if (stone_count >= 1 && wood_count >= 2)
        {
            craftTorchBTN.gameObject.SetActive(true);
        }
        else
        {
            craftTorchBTN.gameObject.SetActive(false);
        }


        // Pickaxe 

        PickaxeReq1.text = "5 Stone [" + stone_count + "]";
        PickaxeReq2.text = "3 Wood [" + wood_count + "]";

        if (stone_count >= 5 && wood_count >= 3)
        {
            craftPickaxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftPickaxeBTN.gameObject.SetActive(false);
        }

        // Spear 

        SpearReq1.text = "1 Stone [" + stone_count + "]";
        SpearReq2.text = "5 Wood [" + wood_count + "]";

        if (stone_count >= 1 && wood_count >= 5)
        {
            craftSpearBTN.gameObject.SetActive(true);
        }
        else
        {
            craftSpearBTN.gameObject.SetActive(false);
        }
    }
}

