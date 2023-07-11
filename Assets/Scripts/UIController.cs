using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Show Inventory")]
    public GameObject InventoryPanel;
    private bool isOpen = false;

    [Header("Add Items in Inventory")]
    public GameObject Items;
    public GameObject ModelItem;
    public List<GameObject> ItemsList;

    [Header("Show Item Details")]
    public Image ItemImage;
    public TMP_Text ItemName;
    public TMP_Text ItemDescription;

    [Header("Show Interaction Dialog")]
    public GameObject InteractionPanel;
    public TMP_Text InteractionText;
    public Image Portrait;


    TextInteractable textInteractable;
    public IEnumerator _coroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            InventoryPanel.SetActive(isOpen);
        }
    }

    public static void SetInventoryImage(Item item)
    {
        if (instance == null)
            return;

        GameObject newItem = Instantiate(instance.ModelItem);
        newItem.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = item.ItemImage;
        newItem.transform.SetParent(instance.Items.transform, false);
        newItem.GetComponent<Button>().onClick.AddListener(() => instance.ShowDetails(newItem));
        instance.ItemsList.Add(newItem);
    }

    public static void SetText(TextInteractable interactable)
    {
        if (instance == null)
            return;

        instance.Portrait.sprite = interactable.PortraitImage;

        if(interactable.ConditionalItem != null)
        {
            if (Inventory.HasItem(interactable.ConditionalItem))
            {
                instance.InteractionText.text = interactable.ConditionalText;
                if (interactable.UseItem)
                {
                    Inventory.UseItem(interactable.ConditionalItem);
                    interactable.OnUseItem.Invoke();
                }
            }
            else
            {
                instance.InteractionText.text = interactable.text;
            }
        }
        else
        {
            instance.InteractionText.text = interactable.text;
        }

        instance.InteractionPanel.SetActive(true);

        instance._coroutine = instance.CloseDialog(interactable.waitTime);
        instance.StartCoroutine(instance._coroutine);

        instance.textInteractable = interactable;
    }

    public static void SetTextCollectable(CollectableItem collectable)
    {
        if (instance == null)
            return;

        instance.Portrait.sprite = collectable.PortraitImage;
        instance.InteractionText.text = collectable.text;
        instance.InteractionPanel.SetActive(true);
        instance._coroutine = instance.CloseDialog(collectable.waitTime);
        instance.StartCoroutine(instance._coroutine);
    }

    public static void DisableInteraction()
    {
        if (instance == null)
            return;

        instance.InteractionPanel.SetActive(false);
        if(instance.textInteractable != null)
            instance.textInteractable.isInteracting = false;
    }

    public static void RemoveInventoryImage(int indexImage)
    {
        if (instance == null)
            return;

        //Debug.Log("Vou remover o item " + indexImage);
        Destroy(instance.ItemsList[indexImage]);
        instance.ItemsList.RemoveAt(indexImage);
    }

    public void ShowDetails(GameObject item)
    {
        if (instance == null)
            return;

        int index = instance.ItemsList.IndexOf(item);
        Item currentItem = Inventory.ShowItem(index);

        if (currentItem != null)
        {
            instance.ItemImage.gameObject.SetActive(true);
            instance.ItemImage.sprite = currentItem.ItemImage;
            instance.ItemName.text = currentItem.ItemName;
            instance.ItemDescription.text = currentItem.Description;
        }
        else
        {
            instance.ItemImage.gameObject.SetActive(false);
            instance.ItemName.text = "";
            instance.ItemDescription.text = "";
        }
    }

    private IEnumerator CloseDialog(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //Debug.Log("Diálogo Fechado");
            DisableInteraction();
            break;
        }
    }

    public static void CancelDialog()
    {
        if (instance == null)
            return;

        if(instance._coroutine != null)
            instance.StopCoroutine(instance._coroutine);
    }
}
