using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public static Inventory instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetItem(Item item)
    {
        if (instance == null)
            return;

        instance.items.Add(item);
        UIController.SetInventoryImage(item);
    }

    public static bool HasItem(Item item)
    {
        if (instance == null)
            return false;

        return instance.items.Contains(item);
    }

    public static void UseItem(Item item)
    {
        if (instance == null)
            return;

        int index = instance.items.IndexOf(item);
        instance.items.Remove(item);

        if(index >= 0)
            UIController.RemoveInventoryImage(index);
    }

    public static Item ShowItem(int index)
    {
        if (instance == null)
            return null;

        Item item = instance.items[index];

        if(item != null)
            return item;

        return null;
    }
}
