using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Item", menuName = "Create new Armor Item")]
[System.Serializable]
public class Armor_Item : Item
{
    public int defense;
    public int speed;
    public int fireProtection;
    public int radProtection;
}

