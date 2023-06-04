using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public List<Item> items;

    private void Awake()
    {
        instance = this;
    }

    public Item GetItemById(int itemId)
    {
        foreach (Item item in items)
        {
            if (item.itemId == itemId)
            {
                return item;
            }
        }

        return null; // Si no se encuentra el item con el ID especificado, retorna null
    }

}
