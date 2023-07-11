using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextInteractable : Interactable
{
    [TextArea(5, 5)]
    public string text;
    public Sprite PortraitImage;

    [TextArea(5, 5)]
    public string ConditionalText;
    public Item ConditionalItem;

    public bool UseItem;
    public UnityEvent OnUseItem;

    public override void Interact()
    {
        if (isInteracting)
            return;

        isInteracting = true;
        UIController.SetText(this);
    }
}
