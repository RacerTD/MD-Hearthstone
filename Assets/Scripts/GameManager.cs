using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CardDeckScript cardDeck;
    public HandScript hand;
    public CardsSideBySide cardsSideBySide;
    public EnemyFieldScript enemyField;
    public ManaScript mana;
    public PlayerFieldScript playerField;
    public LootFildScript lootField;
    public GameObject defeatScreen;
    public GameObject victoryScreen;

    public bool playersTurn;
    public CardType cardInHand = CardType.Nothing;
    public Highlight highlight = Highlight.Nothing;
    public OneCardManager currentlyDragging = null;

    public OneCardManager clicked01 = null;
    public OneCardManager clicked02 = null;
    public OneCardManager abilityUser = null;
    public List<Vector3> particlePosition = new List<Vector3>();

    public int healAbilityCost;
    private int healAbilityEffect;
    public int DMGAbilityCost;
    private int DMGAbilityEffect;

    private AbilityNames abilityToActivate = AbilityNames.nothing;
    public LootScript lootEnabler = null;

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
        PlayerAbility,
        End
    }

    public GameState gameState;

    public static GameManager Main;

    void Awake()
    {
        if(Main != null && Main != this)
        {
            Destroy(this);
        }
        else if(Main == null)
        {
            Main = this;
        }
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
            TriggerEndScreen(true);
        }
        if (Input.GetKeyDown("y"))
        {
            TriggerEndScreen(false);
        }


        switch (gameState)
        {
            case GameState.GameStart:
                cardDeck.SpawnStartCards();
                cardDeck.ShuffleDeck();
                cardDeck.SpawnCardDeck();
                gameState = GameState.Enemy;
                break;

            case GameState.Enemy:
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

    /// <summary>
    /// Aktiviert den Endscreen
    /// </summary>
    /// <param name="death"></param>
    public void TriggerEndScreen(bool death)
    {
        gameState = GameState.End;
        if (death)
        {
            defeatScreen.SetActive(true);
            defeatScreen.GetComponent<Animator>().SetBool("EndGame", true);
        }
        else
        {
            victoryScreen.SetActive(true);
            victoryScreen.GetComponent<Animator>().SetBool("EndGame", true);
        }
        StartCoroutine(GameEnd());

    }

    private IEnumerator GameEnd()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenu");
    }


    private void Clicking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ResetAbilitys();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition; // Camera.main.Screen/ToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //Debug.Log(hit.collider.name);

            if (hit.collider != null && gameState == GameState.PlayerIdle)
            {
                switch (hit.collider.name)
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
                    case "Image":
                        hit.collider.gameObject.transform.GetComponentInParent<OneCardManager>().GiveGameManagerCard();
                        break;
                    case "LowHealLoot(Clone)":
                        abilityToActivate = AbilityNames.lowHeal;
                        lootEnabler = hit.collider.GetComponent<LootScript>();
                        break;
                    case "HighHealLoot(Clone)":
                        abilityToActivate = AbilityNames.highHeal;
                        lootEnabler = hit.collider.GetComponent<LootScript>();
                        break;
                    case "LowDamageLoot(Clone)":
                        abilityToActivate = AbilityNames.lowDMG;
                        lootEnabler = hit.collider.GetComponent<LootScript>();
                        break;
                    case "HighDamageLoot(Clone)":
                        abilityToActivate = AbilityNames.highDMG;
                        lootEnabler = hit.collider.GetComponent<LootScript>();
                        break;
                    default:
                        ResetAbilitys();
                        break;
                }
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
        Debug.Log("Clicked on Card");
        if (lootEnabler != null)
        {
            clickedOn.GetComponent<OneCardManager>().EquipAbility(abilityToActivate);
            lootEnabler.Destroy();
            ResetAbilitys();
        } 
        else if (gameState == GameState.PlayerIdle)
        {
            if (abilityUser != null)
            {
                clicked01 = clickedOn;
                if (healAbilityCost != 0 && healAbilityEffect != 0)
                {
                    Heal();
                }
                else if (DMGAbilityCost != 0 && DMGAbilityEffect != 0)
                {
                    if (clicked01.cardAsset.taunt && enemyField.HasTaunt())
                    {
                        Damage();
                    }
                    else if (!enemyField.HasTaunt())
                    {
                        Damage();
                    }
                }
                else
                {
                    ResetAbilitys();
                    return;
                }
            }
            else
            {
                if (clicked01 == null && clickedOn.cardAsset.cardType == CardType.Human && clickedOn.summoningSickness == false)
                {
                    highlight = Highlight.Attack;
                    clicked01 = clickedOn;
                }
                else if (clicked01 != null && clicked02 == null && (clickedOn.cardAsset.cardType == CardType.Enemy || clickedOn.cardAsset.cardType == CardType.Egg))
                {
                    clicked02 = clickedOn;
                    if (clicked02.cardAsset.taunt && enemyField.HasTaunt())
                    {
                        clicked01.attackUsed = true;
                        clicked01.Damage(clicked02.Attack);
                        clicked02.Damage(clicked01.Attack);
                        clicked01.cardAsset.attackUsed = true;
                        ResetAbilitys();
                    }
                    else if (!enemyField.HasTaunt())
                    {
                        clicked01.attackUsed = true;
                        clicked01.Damage(clicked02.Attack);
                        clicked02.Damage(clicked01.Attack);
                        clicked01.cardAsset.attackUsed = true;
                        ResetAbilitys();
                    }
                    else
                    {
                        ResetAbilitys();
                    }
                }
                else
                {
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
                //particlePosition.Add(clicked01.transform.position);
                clicked01.Damage(clicked02.Attack);
                //particlePosition.Add(clicked02.transform.position);
                clicked02.Damage(clicked01.Attack);
                ResetAbilitys();
            }
        }
    }

    void TurnBegin()
    {
        playerField.TurnBegin();
        //enemyField.TurnStart();
        mana.TurnBegin();
    }


    public void ResetAbilitys()
    {
        clicked01 = null;
        clicked02 = null;
        healAbilityCost = 0;
        healAbilityEffect = 0;
        DMGAbilityCost = 0;
        DMGAbilityEffect = 0;
        abilityUser = null;
        highlight = Highlight.Nothing;
        abilityToActivate = AbilityNames.nothing;
        lootEnabler = null;
    }

    #region Abilitys
    private void Heal()
    {
        if (mana.manaCount >= healAbilityCost && clicked01.Healable())
        {
            clicked01.Heal(healAbilityEffect);
            mana.UsedMana(healAbilityCost);

            //particlePosition.Add(clicked01.transform.position);
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
            //particlePosition.Add(clicked01.transform.position);

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
        //TurnBegin();
        ResetAbilitys();
        lootField.OnEndTurn();
        gameState = GameState.Enemy;
        enemyField.ResetEnemyState();
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