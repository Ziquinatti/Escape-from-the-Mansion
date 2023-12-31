using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    text, collectable, none
}

public abstract class Interactable : MonoBehaviour
{
    public bool isInteracting;
    public ObjectType objectType;
    public float waitTime = 3f;

    public abstract void Interact();
}
