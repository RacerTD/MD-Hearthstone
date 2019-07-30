using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetingOptions
{
    Enemy,
    Human,
    All,
    Nothing

}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CardAsset : ScriptableObject
{
    public string prefabName = "HumanCard";

    [Header("General Info")]
    [Tooltip("Spell, Enemy, Human, Equipment")]
    public string cardType;
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
    public bool healLowUsed = false;
    public int healLowCost;
    public bool healLowEnabled;

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