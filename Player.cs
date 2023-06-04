using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int speed;
    public int fireProtection;
    public int radProtection;

    [Header("Offensive Stats")]
    public int damage;
    public int accuracy;

    [Header("Defensive Stats")]
    public int maxHealth;
    public int currentHealth;
    public int defense;
    public int cutResistance;

    public float timer = 0f;


    private void Awake()
    {
        instance = this;
    }
}