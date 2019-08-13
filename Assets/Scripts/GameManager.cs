﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public HandScript hand;
    public EnemyFieldScript enemyField;
    public ManaScript mana;
    public PlayerFieldScript playerField;

    public bool playersTurn;
    public CardType cardInHand = CardType.Nothing;
    public Highlight highlight = Highlight.Nothing;
    public OneCardManager currentlyDragging = null;

    private OneCardManager clicked01 = null;
    private OneCardManager clicked02 = null;
    private OneCardManager abilityUser = null;

    private int healAbilityCost;
    private int healAbilityEffect;
    private int DMGAbilityCost;
    private int DMGAbilityEffect;

    public enum Highlight
    {
        Attack,
        Heal,
        AOEAttack,
        AOEHeal,
        Nothing
    }

    public enum GameState
    {
        GameStart,
        Enemy,
        PlayerCardDraw,
        PlayerIdle,
        PlayerCardInHand,
        PlayerAttack,
        PlayerAbility
    }

    public GameState gameState;

    void Awake()
    {

    }
    private void Start()
    {
        gameState = GameState.GameStart;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cardDeck.GetComponent<CardDeckScript>().MoveCardToHand();
        }
        if (Input.GetKeyDown("x"))
        {
            enemyField.SpawnNewEnemy();
        }


        switch (gameState)
        {
            case GameState.GameStart:
                //KartenDeck Spawnen
                cardDeck.GetComponent<CardDeckScript>().ShuffleDeck();
                cardDeck.GetComponent<CardDeckScript>().SpawnCardDeck();
                gameState = GameState.Enemy;
                break;

            case GameState.Enemy:
                //Gesamter Enemy Turn
                if (enemyField.GetComponent<Transform>().transform.childCount < 3)
                {
                    enemyField.SpawnNewEnemy();
                    enemyField.SpawnNewEnemy();
                    enemyField.SpawnNewEnemy();
                    //Debug.Log("Spawned Card");
                }
                gameState = GameState.PlayerCardDraw;
                break;

            case GameState.PlayerCardDraw:
                PlayerCardDraw();
                ResetAbilitys();
                TurnBegin();
                break;

            case GameState.PlayerIdle:
                Clicking();
                break;

        }
    }

    private void Clicking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition; // Camera.main.Screen/ToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            Debug.Log(hit.collider.name);

            switch(hit.collider.name)
            {
                
                case "HighDamageAbilitySymbol":
                case "LowDamageAbilitySymbol":
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().DamageAbility();
                    break;
                case "LowHealAbilitySymbol":
                case "HighHealAbilitySymbol":
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().HealAbility();
                    break;
                case "Frame":
                case "Background":
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().GiveGameManagerCard();
                    break;
                default:
                    ResetAbilitys();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 mousePos = Input.mousePosition;

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "Frame")
                {
                    hit.collider.gameObject.GetComponentInParent<NameTooltip>().TooltipSwitch();
                }
            }
        }
    }

    public void CardClicked(OneCardManager clickedOn)
    {
        if (gameState == GameState.PlayerIdle)
        {
            if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
            {
                clicked01 = clickedOn;
                highlight = Highlight.Attack;
                if (healAbilityCost != 0 && healAbilityEffect != 0) //Check for Heal Abilityclicked01.GetComponent<OneCardManager>().
                {
                    Heal();
                }
            }
            else if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
            {
                clicked01 = clickedOn;
                if (DMGAbilityCost != 0 && DMGAbilityEffect != 0) // Check for Damage Ability
                {
                    if (clicked01.cardAsset.taunt && enemyField.taunt)
                    {
                        Damage();
                    }
                    else if (!enemyField.taunt)
                    {
                        Damage();
                    }
                }
                else
                {
                    clicked01 = null;
                }
            }
            else if (clicked01 != null && clickedOn != clicked01 && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
            {
                clicked02 = clickedOn;
            }
            else
            {
                ResetAbilitys();
            }

            if (clicked01 != null && clicked02 != null) //Basic Attack
            {
                if (clicked02.cardAsset.taunt && enemyField.taunt)
                {
                    clicked01.Health = clicked01.Health - clicked02.Attack;
                    clicked02.Health = clicked02.Health - clicked01.Attack;
                    clicked01.cardAsset.attackUsed = true;
                    ResetAbilitys();
                }
                else if (!enemyField.taunt)
                {
                    clicked01.Health = clicked01.Health - clicked02.Attack;
                    clicked02.Health = clicked02.Health - clicked01.Attack;
                    clicked01.cardAsset.attackUsed = true;
                    ResetAbilitys();
                }
            }
        }
        else if (gameState == GameState.Enemy)
        {
            if (clicked01 == null && clicked02 == null)
            {
                clicked01 = clickedOn;
            }
            else if (clicked01 != null && clicked02 == null && clicked01 != clickedOn)
            {
                clicked02 = clickedOn;
            }
            else
            {
                ResetAbilitys();
            }

            if (clicked01 != null && clicked02 != null)
            {
                clicked01.Health = clicked01.Health - clicked02.Attack;
                clicked02.Health = clicked02.Health - clicked01.Attack;
                ResetAbilitys();
            }
        }
    }

    void TurnBegin()
    {
        playerField.TurnBegin();
        mana.TurnBegin();
    }


    private void ResetAbilitys()
    {
        clicked01 = null;
        clicked02 = null;
        healAbilityCost = 0;
        healAbilityEffect = 0;
        DMGAbilityCost = 0;
        DMGAbilityEffect = 0;
        abilityUser = null;
        highlight = Highlight.Nothing;
    }

    #region Abilitys
    private void Heal()
    {
        if (mana.manaCount >= healAbilityCost && clicked01.Healable())
        {
            clicked01.Heal(healAbilityEffect);
            mana.UsedMana(healAbilityCost);

            abilityUser.UsedHeal();
            ResetAbilitys();
        }
        else
        {
            ResetAbilitys();
        }
    }

    private void Damage()
    {
        if (mana.manaCount >= DMGAbilityCost)
        {
            clicked01.Damage(DMGAbilityEffect);
            mana.UsedMana(DMGAbilityCost);

            abilityUser.UsedDamage();
            ResetAbilitys();
        }
        else
        {
            ResetAbilitys();
        }
    }

    public void HealAbility(int heal, int cost, OneCardManager user, bool used)
    {
        if (healAbilityCost == 0 && healAbilityEffect == 0 && !used && cost <= mana.manaCount)
        {
            healAbilityCost = cost;
            healAbilityEffect = heal;
            abilityUser = user;
            highlight = Highlight.Heal;
        }
        else
        {
            ResetAbilitys();
        }
    }

    public void DMGAbility(int DMG, int cost, OneCardManager user, bool used)
    {
        if (DMGAbilityCost == 0 && DMGAbilityEffect == 0 && !used && cost <= mana.manaCount)
        {
            DMGAbilityCost = cost;
            DMGAbilityEffect = DMG;
            abilityUser = user;
            highlight = Highlight.Attack;
        }
        else
        {
            ResetAbilitys();
        }
    }
    #endregion

    public void EndTurn()
    {
        gameState = GameState.Enemy;
    }

    private void PlayerCardDraw()
    {
        if (hand.GetComponent<Transform>().transform.childCount < 5)
        {
            for (int i = hand.GetComponent<Transform>().transform.childCount; i < 5; i++)
            {
                if (cardDeck.GetComponent<Transform>().transform.childCount > 0)
                {
                    cardDeck.GetComponent<CardDeckScript>().MoveCardToHand();
                }
                else
                {
                    break;
                }
            }
        }
        gameState = GameState.PlayerIdle;
    }
}