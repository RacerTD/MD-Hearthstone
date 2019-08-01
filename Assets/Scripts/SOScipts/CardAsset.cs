using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Enemy,
    Human,
    Epuipment,
    Spell
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CardAsset : ScriptableObject
{
    public string prefabName = "Card";

    [Header("General Info")]
    [Tooltip("Spell, Enemy, Human, Equipment")]
    public CardType cardType;
    public Sprite cardImageSmall;
    public Sprite cardImageLarge;

    [Header("Equipment & Humand & Enemys")]
    public int maxHealth;
    public int attack;

    [Header("Equipment & Humand & Spell")]
    public int cost;
    [TextArea(2, 3)]
    public string description;

    [Header("Abilitys")]
    public Ability lowHeal;

    public bool healHighUsed = false;
    public int healHighCost;
    public bool healHighEnabled;

    public bool attackLowUsed = false;
    public int attackLowCost;
    public bool attackLowEnabled;

    public bool attackHighUsed = false;
    public int attackHighCost;
    public bool attackHighEnabled;

}

[System.Serializable]
public class Ability
{
    public bool used = false;
    public int hallo;

}