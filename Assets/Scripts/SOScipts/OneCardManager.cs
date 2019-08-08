using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OneCardManager : MonoBehaviour
{
    public GameObject enemyField;
    public GameObject gameManager;
    public GameObject cardDeck;
    public GameObject manPower;
    public GameObject mana;
    public GameObject hand;
    public CardAsset cardAsset;
    public string prefabName;
    private bool onBoard = false;

    [Header("CardComponents")]
    public Image cardGraphic;
    public List<TextMeshProUGUI> costText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> nameText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> descriptionText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> attackText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> lifeText = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> maxLifeText = new List<TextMeshProUGUI>();

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject spellCardFront;
    public GameObject equipmentCardFront;
    public GameObject humanCardFront;
    public GameObject boardCard;

    [Header("Ability Symbole")]
    public GameObject lowHealGameObject;
    public GameObject highHealGameObject;
    public GameObject lowDMGGameObject;
    public GameObject highDMGGameObject;

    [Header("Spott")]
    public GameObject taunt;

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

    private int equipmentCount;
    //private bool hasAttacked = false;
    //private bool summoningSickness = true;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        enemyField = GameObject.Find("EnemyField");
        cardDeck = GameObject.Find("CardDeck");
        manPower = GameObject.Find("ManPower");
        mana = GameObject.Find("Mana");
        hand = GameObject.Find("Hand");
        if (gameManager.GetComponent<GameManager>().gameState != GameManager.GameState.Enemy)
        {
            cardAsset = cardDeck.GetComponent<CardDeckScript>().CardToSpawn();
        }
        else
        {
            cardAsset = enemyField.GetComponent<EnemyFieldScript>().cardToSpawn();
        }
        InitializeCard();
    }

    void InitializeCard(CardAsset newAsset = null)
    {
        if (gameManager.GetComponent<GameManager>().gameState != GameManager.GameState.Enemy)
        {
            transform.SetParent(cardDeck.transform, false);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            transform.SetParent(enemyField.transform, false);
            transform.localPosition = new Vector3(0, 0, 0);
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
                enemyCardFront.SetActive(true);
                break;
            case CardType.Spell:
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

        UpdateAbilitys();
    }

    public void delete()
    {
        if (onBoard)
        {
            manPower.GetComponent<ManPowerScript>().UsedManPower(- cardAsset.cost);
        }
        
        Destroy(gameObject);
    }

    public void GiveGameManagerCard()
    {
        gameManager.GetComponent<GameManager>().CardClicked(gameObject);
    }

    private void UpdateList(List<TextMeshProUGUI> bla, string value)
    {
        for (int i = 0; i <= bla.Count - 1; i++)
        {
            bla[i].text = value;
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
        if (_health <= 0 && (cardAsset.cardType == CardType.Enemy || cardAsset.cardType == CardType.Human))
        {
            delete();
        }
    }

    public void NowOnField()
    {
        if (!onBoard)
        {
            if (manPower.GetComponent<ManPowerScript>().manPower >= cardAsset.cost)
            {
                humanCardFront.SetActive(false);
                boardCard.SetActive(true);
                manPower.GetComponent<ManPowerScript>().UsedManPower(cardAsset.cost);
                onBoard = true;
            }
            else
            {
                this.gameObject.transform.SetParent(hand.GetComponent<Transform>());
            }
        }
    }

    #region Abilitys
    public void HealAbility()
    {
        if (cardAsset.lowHeal.enabled)
        {
            gameManager.GetComponent<GameManager>().HealAbility(cardAsset.lowHeal.effect, cardAsset.lowHeal.cost);
        }
        else if (cardAsset.highHeal.enabled)
        {
            gameManager.GetComponent<GameManager>().HealAbility(cardAsset.highHeal.effect, cardAsset.highHeal.cost);
        }
    }

    public void Heal(int heal)
    {
        Health = Health + heal;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
    }

    public void DamageAbility()
    {
        if (cardAsset.lowDMG.enabled)
        {
            gameManager.GetComponent<GameManager>().DMGAbility(cardAsset.lowDMG.effect, cardAsset.lowDMG.cost);
        }
        else if (cardAsset.highDMG.enabled)
        {
            gameManager.GetComponent<GameManager>().DMGAbility(cardAsset.highDMG.effect, cardAsset.highDMG.cost);
        }
    }

    public void Damage(int damage)
    {
        Health = Health - damage;
    }
    #endregion
}
