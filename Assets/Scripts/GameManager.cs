using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public GameObject hand;
    public GameObject enemyField;
    public GameObject mana;
    public GameObject playerField;
    public bool playersTurn;
    public CardType cardInHand = CardType.Nothing;
    public Highlight highlight = Highlight.Nothing;
    public GameObject currentlyDragging = null;

    private GameObject clicked01 = null;
    private GameObject clicked02 = null;
    private GameObject abilityUser = null;

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

    public void CardClicked(GameObject clickedOn)
    {
        
        if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
        {
            clicked01 = clickedOn;
            highlight = Highlight.Attack;
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
                if (clicked01.GetComponent<OneCardManager>().cardAsset.taunt && enemyField.GetComponent<EnemyFieldScript>().taunt)
                {
                    Damage();
                } 
                else if (!enemyField.GetComponent<EnemyFieldScript>().taunt)
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
            if (clicked02.GetComponent<OneCardManager>().cardAsset.taunt && enemyField.GetComponent<EnemyFieldScript>().taunt)
            {
                clicked01.GetComponent<OneCardManager>().Health = clicked01.GetComponent<OneCardManager>().Health - clicked02.GetComponent<OneCardManager>().Attack;
                clicked02.GetComponent<OneCardManager>().Health = clicked02.GetComponent<OneCardManager>().Health - clicked01.GetComponent<OneCardManager>().Attack;
                clicked01.GetComponent<OneCardManager>().cardAsset.attackUsed = true;
                ResetAbilitys();
            }
            else if (!enemyField.GetComponent<EnemyFieldScript>().taunt)
            {
                clicked01.GetComponent<OneCardManager>().Health = clicked01.GetComponent<OneCardManager>().Health - clicked02.GetComponent<OneCardManager>().Attack;
                clicked02.GetComponent<OneCardManager>().Health = clicked02.GetComponent<OneCardManager>().Health - clicked01.GetComponent<OneCardManager>().Attack;
                clicked01.GetComponent<OneCardManager>().cardAsset.attackUsed = true;
                ResetAbilitys();
            }
        }
    }

    void TurnBegin()
    {
        playerField.GetComponent<PlayerFieldScript>().TurnBegin();
        mana.GetComponent<ManaScript>().TurnBegin();
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
        if (mana.GetComponent<ManaScript>().manaCount >= healAbilityCost && clicked01.GetComponent<OneCardManager>().Healable())
        {
            clicked01.GetComponent<OneCardManager>().Heal(healAbilityEffect);
            mana.GetComponent<ManaScript>().UsedMana(healAbilityCost);

            abilityUser.GetComponent<OneCardManager>().UsedHeal();
            ResetAbilitys();
        }
        else
        {
            ResetAbilitys();
        }
    }

    private void Damage()
    {
        if (mana.GetComponent<ManaScript>().manaCount >= DMGAbilityCost)
        {
            clicked01.GetComponent<OneCardManager>().Damage(DMGAbilityEffect);
            mana.GetComponent<ManaScript>().UsedMana(DMGAbilityCost);

            abilityUser.GetComponent<OneCardManager>().UsedDamage();
            ResetAbilitys();
        }
        else
        {
            ResetAbilitys();
        }
    }

    public void HealAbility(int heal, int cost, GameObject user, bool used)
    {
        if (healAbilityCost == 0 && healAbilityEffect == 0 && !used && cost <= mana.GetComponent<ManaScript>().manaCount)
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

    public void DMGAbility(int DMG, int cost, GameObject user, bool used)
    {
        if (DMGAbilityCost == 0 && DMGAbilityEffect == 0 && !used && cost <= mana.GetComponent<ManaScript>().manaCount)
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