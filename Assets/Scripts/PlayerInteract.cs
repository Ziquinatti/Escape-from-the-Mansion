using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Interactable item;

    void Update()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
                item.Interact();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.TryGetComponent<Interactable>(out item);
    }

    private void OnTriggerExit(Collider collider)
    {
        item = null;
        UIController.DisableInteraction();
        UIController.CancelDialog();
    }
}
