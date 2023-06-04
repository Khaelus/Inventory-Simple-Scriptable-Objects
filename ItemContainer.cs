using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ItemContainer : MonoBehaviour
{
    public Item itemOnContainer;

    public int quantity;
    public float weight;

    public Image itemSprite;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemWeightText;


    public void OnButtonClick()
    {
        // Mostrar un mensaje de depuración con el ID del ItemContainer
        Debug.Log("ItemContainer ID: " + itemOnContainer.name);
    }

    private void Update()
    {
        weight = itemOnContainer.weight * quantity;
        itemSprite.sprite = itemOnContainer.sprite;

        if(quantity > 1)
        {
            itemNameText.text = $"{itemOnContainer.itemName} ({quantity})";
        }
        else itemNameText.text = $"{itemOnContainer.itemName}";

        itemWeightText.text = weight.ToString();
    }

}

