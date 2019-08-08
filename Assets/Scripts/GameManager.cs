using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public GameObject hand;
    public GameObject enemyField;
    public GameObject mana;
    public bool playersTurn;

    private GameObject clicked01 = null;
    private GameObject clicked02 = null;

    private int healAbilityCost;
    private int healAbilityEffect;
    private int DMGAbilityCost;
    private int DMGAbilityEffect;


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
            enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
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
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    //Debug.Log("Spawned Card");
                }
                gameState = GameState.PlayerCardDraw;
                break;

            case GameState.PlayerCardDraw:
                PlayerCardDraw();
                //Spieler Karten auffüllen
                break;

            case GameState.PlayerIdle:
                PlayerInput();
                break;

        }
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "LowDamageAbilitySymbol" || hit.collider.name == "HighDamageAbilitySymbol")
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().DamageAbility();
                if (hit.collider.name == "LowHealAbilitySymbol" || hit.collider.name == "HighHealAbilitySymbol")
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().HealAbility();
                if (hit.collider.name == "Frame")
                    hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().GiveGameManagerCard();
            }
        }
    }

    public void CardClicked(GameObject clickedOn)
    {
        
        if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
        {
            clicked01 = clickedOn;
            if (healAbilityCost != 0 && healAbilityEffect != 0) //Check for Heal Ability
            {
                Heal();
            }
        }
        else if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
        {
            clicked01 = clickedOn;
            if (DMGAbilityCost != 0 && DMGAbilityEffect != 0) // Check for Damage Ability
            {
                Damage();
            }
            else
            {
                clicked01 = null;
            }
        }
        else if (clicked01 != null && clickedOn != clicked01 && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
        {
            clicked02 = clickedOn;
        } else
        {
            ResetAbilitys();
        }

        if (clicked01 != null && clicked02 != null) //Basic Attack
        {
            clicked01.GetComponent<OneCardManager>().Health = clicked01.GetComponent<OneCardManager>().Health - clicked02.GetComponent<OneCardManager>().Attack;
            clicked02.GetComponent<OneCardManager>().Health = clicked02.GetComponent<OneCardManager>().Health - clicked01.GetComponent<OneCardManager>().Attack;
            ResetAbilitys();
        }
    }

    private void ResetAbilitys()
    {
        clicked01 = null;
        clicked02 = null;
        healAbilityCost = 0;
        healAbilityEffect = 0;
        DMGAbilityCost = 0;
        DMGAbilityEffect = 0;
    }

    #region Abilitys
    private void Heal()
    {
        if (mana.GetComponent<ManaScript>().manaCount >= healAbilityCost)
        {
            clicked01.GetComponent<OneCardManager>().Heal(healAbilityEffect);
            mana.GetComponent<ManaScript>().UsedMana(healAbilityCost);
            ResetAbilitys();
        }
        else
        {
            //Debug.Log("Bärenkatapult");
            ResetAbilitys();
        }
    }

    private void Damage()
    {
        if (mana.GetComponent<ManaScript>().manaCount >= DMGAbilityCost)
        {
            clicked01.GetComponent<OneCardManager>().Damage(DMGAbilityEffect);
            ResetAbilitys();
        }
        else
        {
            ResetAbilitys();
        }
    }

    public void HealAbility(int heal, int cost)
    {
        if (healAbilityCost == 0 && healAbilityEffect == 0)
        {
            healAbilityCost = cost;
            healAbilityEffect = heal;
        }
        else
        {
            ResetAbilitys();
        }
    }

    public void DMGAbility(int DMG, int cost)
    {
        if (DMGAbilityCost == 0 && DMGAbilityEffect == 0)
        {
            DMGAbilityCost = cost;
            DMGAbilityEffect = DMG;
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
        if (hand.GetComponent<Transform>().transform.childCount < 2)
        {
            for (int i = hand.GetComponent<Transform>().transform.childCount; i < 2; i++)
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