using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    [TextArea(5,5)]
    public string Description;
}
