using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public int itemId;

    [Header("General")]
    public Type selectedType;

    [Header("Details")]
    public string itemName;
    public string description;
    public Sprite sprite;

    [Header("Stats")]
    public float weight;
    public int value;

    [Header("Stacking")]
    public bool canStack;


    public enum Type
    {
        Aid,
        Ammo,
        Armor,
        Weapon,
        Mats,

    }
}
