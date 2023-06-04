using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class BackpackData
{
    public string backpackName;
    public float currentWeight;
    public float maxWeight;
    public List<ItemContainerData> itemContainerDataList;
}

[Serializable]
public class ItemContainerData
{
    public int itemId;
    public int quantity;
    public int equippedType; // Nuevo campo para almacenar el tipo de equipamiento (0: Sin equipar, 1: Armadura)
    //public bool isWeaponEquipped; //colocar en la logica de save y load
}

public class Backpack : MonoBehaviour
{
    public static Backpack instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform itemContainerParent;
    public ItemContainer itemContainerPrefab;

    public string backpackName;
    public float currentWeight;
    public float maxWeight;

    public bool heavyLoad;

    public List<ItemContainer> itemContainerList;

    private void Update()
    {
        if (currentWeight > maxWeight)
        {
            heavyLoad = true;
        }
        else
        {
            heavyLoad = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddThisItem();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveItems();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadItems();
        }
    }

    public void AddThisItem()
    {
        AddItem(ItemDatabase.instance.items[0], 2);
        AddItem(ItemDatabase.instance.items[1], 1);
        AddItem(ItemDatabase.instance.items[2], 1);
        AddItem(ItemDatabase.instance.items[3], 1);
        AddItem(ItemDatabase.instance.items[4], 1);
        AddItem(ItemDatabase.instance.items[5], 1);
    }

    public void AddItem(Item item, int quantity)
    {
        // Busca si hay un ItemContainer existente para el elemento apilable
        ItemContainer stackableContainer = FindStackableContainer(item);

        if (stackableContainer != null && item.canStack)
        {
            // Si se encuentra un contenedor apilable, se suma la cantidad
            stackableContainer.quantity += quantity;
        }
        else
        {
            // Si no se encuentra un contenedor apilable o el elemento no puede apilarse,
            // se crea un nuevo contenedor
            // Crea una instancia del prefab del contenedor del item
            ItemContainer newItemContainer = Instantiate(itemContainerPrefab, itemContainerParent);

            // Asigna el item al contenedor del item
            newItemContainer.itemOnContainer = item;
            newItemContainer.quantity = quantity;

            // Añade el contenedor del item a la lista de contenedores en el script del inventario
            itemContainerList.Add(newItemContainer);
        }

        // Verifica si el elemento ya existe en la lista itemList
        bool itemExists = false;
    }

    private ItemContainer FindStackableContainer(Item item)
    {
        foreach (ItemContainer itemContainer in itemContainerList)
        {
            if (itemContainer.itemOnContainer.itemId == item.itemId && itemContainer.itemOnContainer.canStack)
            {
                return itemContainer;
            }
        }

        return null;
    }

    public void SaveItems()
    {
        // Crea una lista para almacenar los datos de los ItemContainer
        List<ItemContainerData> containerDataList = new List<ItemContainerData>();

        // Recorre la lista de contenedores de elementos y guarda los datos de cada uno
        foreach (ItemContainer itemContainer in itemContainerList)
        {
            ItemContainerData containerData = new ItemContainerData
            {
                itemId = itemContainer.itemOnContainer.itemId,
                quantity = itemContainer.quantity,
                equippedType = itemContainer.equippedType // Guardar el tipo de equipamiento
            };

            containerDataList.Add(containerData);
        }

        // Crea una instancia de la clase BackpackData
        BackpackData backpackData = new BackpackData
        {
            backpackName = backpackName,
            currentWeight = currentWeight,
            maxWeight = maxWeight,
            itemContainerDataList = containerDataList
        };

        // Convierte la clase BackpackData a formato JSON
        string json = JsonUtility.ToJson(backpackData);

        // Guarda el JSON en un archivo
        File.WriteAllText("backpack.json", json);

        Debug.Log("Items saved.");
    }

    public void LoadItems()
    {
        if (File.Exists("backpack.json"))
        {
            // Lee el contenido del archivo JSON
            string json = File.ReadAllText("backpack.json");

            // Convierte el JSON a una instancia de la clase BackpackData
            BackpackData backpackData = JsonUtility.FromJson<BackpackData>(json);

            // Asigna los valores del objeto BackpackData a las variables correspondientes
            backpackName = backpackData.backpackName;
            currentWeight = backpackData.currentWeight;
            maxWeight = backpackData.maxWeight;

            // Limpia las listas existentes
            ClearItemContainers();

            // Recorre la lista de datos de ItemContainer y crea los contenedores correspondientes
            foreach (ItemContainerData containerData in backpackData.itemContainerDataList)
            {
                // Busca el Item correspondiente en la base de datos
                Item item = ItemDatabase.instance.GetItemById(containerData.itemId);

                if (item != null)
                {
                    // Crea una instancia del prefab del contenedor del item
                    ItemContainer newItemContainer = Instantiate(itemContainerPrefab, itemContainerParent);

                    // Asigna el item al contenedor del item
                    newItemContainer.itemOnContainer = item;
                    newItemContainer.quantity = containerData.quantity;
                    newItemContainer.equippedType = containerData.equippedType;


                    // Añade el contenedor del item a la lista de contenedores en el script del inventario
                    itemContainerList.Add(newItemContainer);
                }
            }

            Debug.Log("Items loaded.");
        }
        else
        {
            Debug.Log("No saved items found.");
        }
    }


    private void ClearItemContainers()
    {
        foreach (ItemContainer itemContainer in itemContainerList)
        {
            Destroy(itemContainer.gameObject);
        }

        itemContainerList.Clear();
    }
}
