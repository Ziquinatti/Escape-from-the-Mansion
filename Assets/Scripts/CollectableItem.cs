using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Interactable
{
    public Item item;
    [TextArea(5, 5)]
    public string text;
    public Sprite PortraitImage;

    public override void Interact()
    {
        Inventory.SetItem(item);
        //Debug.Log("Coletou " + item.ItemName);
        UIController.SetTextCollectable(this);
        Destroy(gameObject);
    }
}
