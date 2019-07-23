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

public class CardAsset : ScriptableObject
{
    [Header("General Info")]
    [Tooltip("Spell, Enemy, Human, Equipment")]
    public string cardType;
    public Sprite cardImageSmall;
    public Sprite cardImageLarge;
    public string cardTitle;

    [Header("Equipment & Humand & Enemys")]
    public int health;
    public int macHealth;
    public int attack;

    [Header("Equipment & Humand & Spell")]
    public int cost;
    public string AbilityScriptName;
    [TextArea(2, 3)]
    public string description;

}