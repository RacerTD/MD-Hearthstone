﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OneCardManager : MonoBehaviour
{
    public EnemyFieldScript enemyField;
    public GameManager gameManager;
    public CardDeckScript cardDeck;
    public ManPowerScript manPower;
    public ManaScript mana;
    public HandScript hand;

    public CardAsset cardAsset;
    public string prefabName;
    public bool onBoard = false;
    public int equipmentCount = 0;
    
    [Header("Particle Systems")]
    public GameObject healParticles;
    public GameObject humanDamageParticles;

    [Header("CardComponents")]
    public List<Image> boardCardImage = new List<Image>();
    public List<Image> cardGraphic = new List<Image>(); //
    public List<TextMeshProUGUI> costText = new List<TextMeshProUGUI>(); //
    public List<TextMeshProUGUI> nameText = new List<TextMeshProUGUI>(); //
    public List<TextMeshProUGUI> descriptionText = new List<TextMeshProUGUI>(); //
    public List<TextMeshProUGUI> attackText = new List<TextMeshProUGUI>(); //
    public List<TextMeshProUGUI> lifeText = new List<TextMeshProUGUI>(); //
    public List<TextMeshProUGUI> maxLifeText = new List<TextMeshProUGUI>(); //
    public List<GameObject> damageNumbers = new List<GameObject>();

    public List<BoxCollider2D> abilityCollider = new List<BoxCollider2D>();

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject spellCardFront;
    public GameObject equipmentCardFront;
    public GameObject humanCardFront;
    public GameObject boardCard;
    public List<GameObject> manaShow = new List<GameObject>();

    [Header("Ability Symbole")]
    public GameObject lowHealGameObject;
    public GameObject highHealGameObject;
    public GameObject lowDMGGameObject;
    public GameObject highDMGGameObject;

    [Header("Spott")]
    public GameObject taunt;
    public int childCount = 4;

    public Vector3 targetPosition;
    public Vector3 targetRotation;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            CheckIfDead();
            UIUpdate();
        }
    }
    public int _health;

    public int Attack
    {
        get { return _attack; }
        set
        {
            _attack = value;
            UIUpdate();
        }
    }
    public int _attack;
    public int maxHealth;

    public int cost;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyField = GameObject.Find("EnemyField").GetComponent<EnemyFieldScript>();
        cardDeck = GameObject.Find("CardDeck").GetComponent<CardDeckScript>();
        manPower = GameObject.Find("ManPower").GetComponent<ManPowerScript>();
        mana = GameObject.Find("Mana").GetComponent<ManaScript>();
        hand = GameObject.Find("Hand").GetComponent<HandScript>();

        if (gameManager.gameState != GameManager.GameState.Enemy)
        {
            cardAsset = cardDeck.CardToSpawn();
        }
        else
        {
            cardAsset = enemyField.cardToSpawn();
        }
        InitializeCard();
    }
    private void Update()
    {

        if (transform.childCount != childCount)
        {
            childCount = transform.childCount;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<OneCardManager>())
                {
                    if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cardType == CardType.Epuipment && cardAsset.cardType == CardType.Human)
                    {
                        EquipEquipment(transform.GetChild(i).GetComponent<OneCardManager>().cardAsset);
                        transform.GetChild(i).GetComponent<OneCardManager>().DeleteEquipment();
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<OneCardManager>().BackToHand();
                    }
                }
            }
        }
    }

    public void DeleteEquipment()
    {
        Destroy(gameObject);
    }

    public void EquipEquipment(CardAsset equipment)
    {
        Debug.Log("Equipment got Equipped");
        maxHealth += equipment.maxHealth;
        Health += equipment.maxHealth;
        Attack += equipment.attack;
        equipmentCount++;

        if (equipment.highHeal.enabled)
        {
            cardAsset.highHeal.enabled = true;
        }
        else if (equipment.highDMG.enabled)
        {
            cardAsset.highDMG.enabled = true;
        }
        else if (equipment.lowHeal.enabled)
        {
            cardAsset.lowHeal.enabled = true;
        }
        else if (equipment.lowDMG.enabled)
        {
            cardAsset.lowDMG.enabled = true;
        }

        UpdateAbilitys();
    }

    public void BackToHand()
    { 
        transform.SetParent(hand.transform);
    }

    void InitializeCard(CardAsset newAsset = null)
    {
        if (gameManager.gameState != GameManager.GameState.Enemy)
        {
            transform.SetParent(cardDeck.transform, false);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.SetParent(enemyField.transform, false);
            transform.localPosition = new Vector3(0, 1000, 0);
        }

        if (newAsset == null && cardAsset == null)
        {
            return;
        }
        if (newAsset == null)
        {
            newAsset = cardAsset;
        }

        switch (newAsset.cardType)
        {
            case CardType.Enemy:
            case CardType.Egg:
                enemyCardFront.SetActive(true);
                break;

            case CardType.AOEDMGSpell:
            case CardType.AOEHealSpell:
                spellCardFront.SetActive(true);
                break;

            case CardType.Epuipment:
                equipmentCardFront.SetActive(true);
                break;

            case CardType.Human:
                humanCardFront.SetActive(true);
                break;
        }

        Attack = newAsset.attack;
        maxHealth = newAsset.maxHealth;
        Health = maxHealth;
        cost = newAsset.cost;

        UpdateList(costText, cost.ToString());
        UpdateList(nameText, cardAsset.name);
        UpdateList(descriptionText, cardAsset.description);
        UpdateList(attackText, Attack.ToString());
        UpdateList(lifeText, _health.ToString());
        UpdateList(maxLifeText, maxHealth.ToString());

        for (int i = 0; i < cardGraphic.Count; i++)
        {
            cardGraphic[i].sprite = cardAsset.cardImage;
        }
        for (int i = 0; i < boardCardImage.Count; i++)
        {
            boardCardImage[i].sprite = cardAsset.boardCardImage;
        }

        UpdateAbilitys();
    }

    public bool Healable()
    {
        if (maxHealth > _health)
        {
            //Debug.Log("Healable");
            return true;
        }
        else
        {
            //Debug.Log("Not Healable");
            return false;
        }
    }

    public void delete()
    {
        if (onBoard)
        {
            manPower.UsedManPower(-cardAsset.cost);
        }
        Destroy(gameObject);
    }

    public void GiveGameManagerCard()
    {
        if (gameManager.highlight == GameManager.Highlight.Heal || gameManager.highlight == GameManager.Highlight.Attack)
        {
            gameManager.CardClicked(this);
        }
        else if (cardAsset.attackUsed == false)
        {
            gameManager.CardClicked(this);
        }
    }

    private void UpdateList(List<TextMeshProUGUI> bla, string value)
    {
        for (int i = 0; i <= bla.Count - 1; i++)
        {
            bla[i].text = value;
        }
    }

    public void TurnBegin()
    {
        //Debug.Log("Baum3");
        cardAsset.lowHeal.used = false;
        cardAsset.highHeal.used = false;
        cardAsset.lowDMG.used = false;
        cardAsset.highDMG.used = false;
        cardAsset.attackUsed = false;
        ChangeSummoningSickness();
    }

    public void ChangeSummoningSickness()
    {
        cardAsset.summoningSickness = false;
        for (int i = 0; i < abilityCollider.Count; i++)
        {
            abilityCollider[i].enabled = true;
        }
        
        if (cardAsset.cardType == CardType.Egg)
        {
            cardAsset = enemyField.strongEnemyCards[Random.Range(0, enemyField.strongEnemyCards.Count)];
            UpdateList(costText, cost.ToString());
            UpdateList(nameText, cardAsset.name);
            UpdateList(descriptionText, cardAsset.description);
            UpdateList(attackText, Attack.ToString());
            UpdateList(lifeText, _health.ToString());
            UpdateList(maxLifeText, maxHealth.ToString());

            for (int i = 0; i < cardGraphic.Count; i++)
            {
                cardGraphic[i].sprite = cardAsset.cardImage;
            }
            for (int i = 0; i < boardCardImage.Count; i++)
            {
                boardCardImage[i].sprite = cardAsset.boardCardImage;
            }
        }
    }
    private void UpdateAbilitys()
    {
        if (cardAsset.lowHeal.enabled)
        {
            lowHealGameObject.SetActive(true);
        }
        else if (cardAsset.highHeal.enabled)
        {
            highHealGameObject.SetActive(true);
        }
        else
        {
            highHealGameObject.SetActive(false);
            lowHealGameObject.SetActive(false);
        }

        if (cardAsset.lowDMG.enabled)
        {
            lowDMGGameObject.SetActive(true);
        }
        else if (cardAsset.highDMG.enabled)
        {
            highDMGGameObject.SetActive(true);
        }
        else
        {
            highDMGGameObject.SetActive(false);
            lowDMGGameObject.SetActive(false);
        }
    }

    private void UIUpdate()
    {
        UpdateList(attackText, _attack.ToString());
        UpdateList(lifeText, _health.ToString());
        UpdateList(maxLifeText, maxHealth.ToString());
    }

    private void CheckIfDead()
    {
        if (_health <= 0 && (cardAsset.cardType == CardType.Enemy || cardAsset.cardType == CardType.Human || cardAsset.cardType == CardType.Egg))
        {
            delete();
        }
    }

    public void NowOnField()
    {
        if (!onBoard)
        {
            if (manPower.manPower >= cardAsset.cost)
            {
                humanCardFront.SetActive(false);
                boardCard.SetActive(true);
                manPower.UsedManPower(cardAsset.cost);
                onBoard = true;
                ManPowerCost();
                gameObject.GetComponent<Draggable>().setsDraggableFalse = true;
                gameObject.GetComponent<Draggable>().Dragable = false;
                TurnBegin();

                for (int i = 0; i < abilityCollider.Count; i++)
                {
                    abilityCollider[i].enabled = false;
                }
                cardAsset.summoningSickness = true;
            }
            else if (cardAsset.cardType == CardType.Epuipment)
            {
                //Werte anrechnen
                Debug.Log("Equpment angelegt");
            }
            else
            {
                this.gameObject.transform.SetParent(hand.GetComponent<Transform>());
            }
        }
    }

    private void ManPowerCost()
    {
        if (cardAsset.cost == 0)
        {
            manaShow[0].SetActive(true);
        }
        else if (cardAsset.cost == 1)
        {
            manaShow[1].SetActive(true);
        }
        else if (cardAsset.cost == 2)
        {
            manaShow[2].SetActive(true);
        }
        else if (cardAsset.cost == 3)
        {
            manaShow[3].SetActive(true);
        }
        else if (cardAsset.cost == 4)
        {
            manaShow[4].SetActive(true);
        }
    }

    public void ShowDamageNumber(int number)
    {
        switch (number)
        {
            case 0:
                break;
            case 1:
                Instantiate(damageNumbers[1], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 2:
                Instantiate(damageNumbers[2], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 3:
                Instantiate(damageNumbers[3], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 4:
                Instantiate(damageNumbers[4], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 5:
                Instantiate(damageNumbers[5], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 6:
                Instantiate(damageNumbers[6], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 7:
                Instantiate(damageNumbers[7], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 8:
                Instantiate(damageNumbers[8], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 9:
                Instantiate(damageNumbers[9], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 10:
                Instantiate(damageNumbers[10], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 11:
                Instantiate(damageNumbers[11], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 12:
                Instantiate(damageNumbers[12], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 13:
                Instantiate(damageNumbers[13], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 14:
                Instantiate(damageNumbers[14], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 15:
                Instantiate(damageNumbers[15], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 16:
                Instantiate(damageNumbers[16], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 17:
                Instantiate(damageNumbers[17], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 18:
                Instantiate(damageNumbers[18], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 19:
                Instantiate(damageNumbers[19], gameManager.particlePosition[0], Quaternion.identity);
                break;
            case 20:
                Instantiate(damageNumbers[20], gameManager.particlePosition[0], Quaternion.identity);
                break;
            default:
                break;
        }
    }


    #region Abilitys

    public void HealAbility()
    {
        //Debug.Log("Heal Called");
        if (cardAsset.lowHeal.enabled)
        {
            //Debug.Log("HealEnabled");
            //Debug.Log(cardAsset.lowHeal.used);
            gameManager.HealAbility(cardAsset.lowHeal.effect, cardAsset.lowHeal.cost, this, cardAsset.lowHeal.used);
        }
        else if (cardAsset.highHeal.enabled)
        {
            gameManager.HealAbility(cardAsset.highHeal.effect, cardAsset.highHeal.cost, this, cardAsset.highHeal.used);
        }
    }

    public void UsedHeal()
    {
        cardAsset.lowHeal.used = true;
        cardAsset.highHeal.used = true;
    }

    public void Heal(int heal)
    {
        Health = Health + heal;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        Instantiate(healParticles, gameObject.transform.localPosition, Quaternion.identity);
    }

    public bool HealAbilityAvailible()
    {
        if ((cardAsset.lowHeal.used || cardAsset.highHeal.used) && cardAsset.cardType == CardType.Human)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DamageAbility()
    {
        if (cardAsset.lowDMG.enabled)
        {
            gameManager.DMGAbility(cardAsset.lowDMG.effect, cardAsset.lowDMG.cost, this, cardAsset.lowDMG.used);
        }
        else if (cardAsset.highDMG.enabled)
        {
            gameManager.DMGAbility(cardAsset.highDMG.effect, cardAsset.highDMG.cost, this, cardAsset.highDMG.used);
        }
    }
    public void UsedDamage()
    {
        cardAsset.lowDMG.used = true;
        cardAsset.highDMG.used = true;
    }

    public void Damage(int damage)
    {
        ShowDamageNumber(damage);
        Health = Health - damage;
        Instantiate(humanDamageParticles, gameObject.transform.localPosition, Quaternion.identity);
    }

    public bool AttackAbilityAvailible()
    {
        if (cardAsset.lowDMG.used || cardAsset.highDMG.used)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
