using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;


public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance { get; set; }

    public GameObject InteractionInfo;
    public float interactRange = 4;
    TextMeshProUGUI interaction_text;
    public bool onTarget;
    public GameObject selectedObject;

    public GameObject selectedTree;
    public GameObject chopHolder;


    private void Start()
    {
        onTarget = false;
        interaction_text = InteractionInfo.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*void Update()
    {
        //Debug.Log(onTarget);

        // Tree chopping

        if (!PauseMenu.isPaused) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                var selectionTransform = hit.transform;
                InteractableObject interactable = selectionTransform.GetComponentInChildren<InteractableObject>();

                ChopableTree chopableTree = selectionTransform.GetComponent<ChopableTree>();

            if (chopableTree && chopableTree.playerInRange)
            {
                chopableTree.canBeChopped = true;
                selectedTree = chopableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
            }
            else
            {
                if (selectedTree != null && selectedTree.GetComponent<ChopableTree>().canBeChopped)
                {
                    selectedTree.gameObject.GetComponent<ChopableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }

            // Rabbits

            RabbitHealth rabbitHealth = selectionTransform.GetComponent<RabbitHealth>();

            if (rabbitHealth && rabbitHealth.playerInRange)
            {
                onTarget = true;
                selectedObject = rabbitHealth.gameObject;
                InteractionInfo.SetActive(true);
                

                if(Input.GetMouseButtonDown(0) && EquipSystem.Instance.IsHoldingWeapon())
                {
                        StartCoroutine(DealDamageTo(rabbitHealth, 0.3f, EquipSystem.Instance.GetWeaponDamage()));
                }
                
            }
            else
            {
                onTarget = false;
                interaction_text.text = "";
                InteractionInfo.SetActive(false);
            }


            //Materials

            if (interactable && interactable.playerInRange)
                {
                    onTarget = true;
                    selectedObject = interactable.gameObject;             
                    interaction_text.text = interactable.GetItemName();
                    InteractionInfo.SetActive(true);
                }
                else
                {
                    onTarget = false;
                    InteractionInfo.SetActive(false);
                }
            }
            else
            {
                onTarget = false;
                InteractionInfo.SetActive(false);
            }


        }
    }*/

    void Update()
    {
        //Debug.Log(onTarget);

        if (!PauseMenu.isPaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                var selectionTransform = hit.transform;
                InteractableObject interactable = selectionTransform.GetComponentInChildren<InteractableObject>();
                ChopableTree chopableTree = selectionTransform.GetComponent<ChopableTree>();

                // chopable tree
                
                if (chopableTree && chopableTree.playerInRange)
                {
                    chopableTree.canBeChopped = true;
                    selectedTree = chopableTree.gameObject;
                    chopHolder.gameObject.SetActive(true);
                }
                else
                {
                    if (selectedTree != null && selectedTree.GetComponent<ChopableTree>().canBeChopped)
                    {
                        selectedTree.GetComponent<ChopableTree>().canBeChopped = false;
                        selectedTree = null;
                        chopHolder.gameObject.SetActive(false);
                    }
                }

                // Rabbits

                RabbitHealth rabbitHealth = selectionTransform.GetComponent<RabbitHealth>();

                if (rabbitHealth && rabbitHealth.playerInRange)
                {
                    onTarget = true;
                    selectedObject = rabbitHealth.gameObject;
                    InteractionInfo.SetActive(true);


                    if (Input.GetMouseButtonDown(0) && EquipSystem.Instance.IsHoldingWeapon())
                    {
                        StartCoroutine(DealDamageTo(rabbitHealth, 0.3f, EquipSystem.Instance.GetWeaponDamage()));
                    }

                }
                else
                {
                    onTarget = false;
                    interaction_text.text = "";
                    InteractionInfo.SetActive(false);
                }

                // materials

                if (interactable && interactable.playerInRange)
                {
                    onTarget = true;
                    selectedObject = interactable.gameObject;
                    interaction_text.text = interactable.GetItemName();
                    InteractionInfo.SetActive(true);
                }
                else
                {
                    onTarget = false;
                    InteractionInfo.SetActive(false);
                }
            }
            else
            {
                if (selectedTree != null && selectedTree.GetComponent<ChopableTree>().canBeChopped)
                {
                    selectedTree.GetComponent<ChopableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }

                onTarget = false;
                InteractionInfo.SetActive(false);
            }
        }
    }

    IEnumerator DealDamageTo(RabbitHealth rabbitHealth, float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        rabbitHealth.TakeDamage(damage);
    }

    public void DisableSelection()
    {
        // handIcon enabled = false;
        //FirstPersonController.instance.crosshair = false;
        InteractionInfo.SetActive(false);

        selectedObject = null;
    }

    public void EnableSelection()
    {
        // handIcon enabled = true;
        //FirstPersonController.instance.crosshair = true;
        InteractionInfo.SetActive(true);

    }
}

