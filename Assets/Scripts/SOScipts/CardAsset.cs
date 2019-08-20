﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Enemy,
    Human,
    Epuipment,
    AOEDMGSpell,
    AOEHealSpell,
    Egg,
    Nothing
}

public enum AbilityNames
{
    lowHeal,
    highHeal,
    lowDMG,
    highDMG,
    nothing
}

[CreateAssetMenu(fileName = "Data", menuName = "Create New Card", order = 1)]
public class CardAsset : ScriptableObject
{
    public string prefabName = "Card";

    [Header("General Info")]
    [Tooltip("Spell, Enemy, Human, Equipment")]
    public CardType cardType;
    public Sprite cardImage; //
    public Sprite boardCardImage; //

    [Header("Equipment & Humand & Enemys")]
    public int maxHealth; //
    public int attack; //
    public bool attackUsed;
    public bool summoningSickness;
    public bool taunt;

    [Header("Equipment & Humand & Spell")]
    public int cost; //
    [TextArea(2, 3)]
    public string description; //

    [Header("Abilitys")]
    public Ability lowHeal;
    public Ability highHeal;
    public Ability lowDMG;
    public Ability highDMG;
}

[System.Serializable]
public class Ability
{
    public bool enabled = false;
    public int effect;
    public bool used = false;
    public int cost;
    public AbilityNames name;
}