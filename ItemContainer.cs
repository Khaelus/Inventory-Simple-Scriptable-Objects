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

    public int equippedType; // Nuevo campo para almacenar el tipo de equipamiento (0: Sin equipar, 1: Armadura, 2: weapon)


    public void OnButtonClick()
    {
        EquipArmor();
        EquipWeapon();
    }

   

    public void EquipArmor()
    {
        if (itemOnContainer.selectedType == Item.Type.Armor)
        {
            // Si la armadura ya está equipada, se desequipa
            if (equippedType == 1)
            {
                equippedType = 0;
                Player.instance.defense = 0;
            }
            else
            {
                // Desequipar cualquier item de armadura previamente equipado
                foreach (ItemContainer itemContainer in Backpack.instance.itemContainerList)
                {
                    if (itemContainer.itemOnContainer.selectedType == Item.Type.Armor && itemContainer.equippedType == 1)
                    {
                        itemContainer.equippedType = 0;
                        // Otra lógica relacionada con el desequipamiento de armadura
                    }
                }

                // Equipar el nuevo item de armadura
                equippedType = 1;
                Player.instance.defense = (itemOnContainer as Armor_Item).defense;
                // Otra lógica relacionada con el equipamiento de armadura
            }

            // Actualizar el texto "equipped" en el nombre del item
            UpdateUI();
        }
    }

    public void EquipWeapon()
    {
        if (itemOnContainer.selectedType == Item.Type.Weapon)
        {
            // Si la Weapon ya está equipada, se desequipa
            if (equippedType == 2) //2: Weapon
            {
                equippedType = 0;
                Player.instance.damage = 0;
            }
            else
            {
                // Desequipar cualquier item de Weapon previamente equipado
                foreach (ItemContainer itemContainer in Backpack.instance.itemContainerList)
                {
                    if (itemContainer.itemOnContainer.selectedType == Item.Type.Weapon && itemContainer.equippedType == 2)
                    {
                        itemContainer.equippedType = 0;
                        // Otra lógica relacionada con el desequipamiento de Weapon
                    }
                }

                // Equipar el nuevo item de Weapon
                equippedType = 2;
                Player.instance.damage = (itemOnContainer as Weapon_Item).damage;
                // Otra lógica relacionada con el equipamiento de Weapon
            }

            // Actualizar el texto "equipped" en el nombre del item
            UpdateUI();
        }
    }




    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        weight = itemOnContainer.weight * quantity;
        itemSprite.sprite = itemOnContainer.sprite;
        itemWeightText.text = weight.ToString();

        itemNameText.text = GetItemNameText();
    }

    private string GetItemNameText()
    {
        if (quantity > 1)
        {
            if (equippedType == 1 && itemOnContainer.selectedType == Item.Type.Armor)
            {
                return $"{itemOnContainer.itemName} ({quantity}) - equipped";
            }
            else if (equippedType == 2 && itemOnContainer.selectedType == Item.Type.Weapon)
            {
                return $"{itemOnContainer.itemName} ({quantity}) - equipped";
            }
            else
            {
                return $"{itemOnContainer.itemName} ({quantity})";
            }
        }
        else
        {
            if (equippedType == 1 && itemOnContainer.selectedType == Item.Type.Armor)
            {
                return $"{itemOnContainer.itemName} - equipped";
            }
            else if (equippedType == 2 && itemOnContainer.selectedType == Item.Type.Weapon)
            {
                return $"{itemOnContainer.itemName} - equipped";
            }
            else
            {
                return itemOnContainer.itemName;
            }
        }
    }



}
