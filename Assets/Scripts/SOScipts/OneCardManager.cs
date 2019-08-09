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
    public int equipmentCount = 0;

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
    public List<GameObject> manaShow = new List<GameObject>();

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
    private void Update()
    {

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
            manPower.GetComponent<ManPowerScript>().UsedManPower(-cardAsset.cost);
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

    public void TurnBegin()
    {
        //Debug.Log("Baum3");
        cardAsset.lowHeal.used = false;
        cardAsset.highHeal.used = false;
        cardAsset.lowDMG.used = false;
        cardAsset.highDMG.used = false;
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
                ManPowerCost();
                gameObject.GetComponent<Draggable>().setsDraggableFalse = true;
                gameObject.GetComponent<Draggable>().Dragable = false;
                TurnBegin();
            }
            else
            {
                this.gameObject.transform.SetParent(hand.GetComponent<Transform>());
            }
        }
    }

    private void ManPowerCost()
    {
        if (cardAsset.cost == 1)
        {
            manaShow[0].SetActive(true);
        }
        else if (cardAsset.cost == 2)
        {
            manaShow[1].SetActive(true);
        }
        else if (cardAsset.cost == 3)
        {
            manaShow[2].SetActive(true);
        }
        else if (cardAsset.cost == 4)
        {
            manaShow[3].SetActive(true);
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
            gameManager.GetComponent<GameManager>().HealAbility(cardAsset.lowHeal.effect, cardAsset.lowHeal.cost, this.gameObject, cardAsset.lowHeal.used);
        }
        else if (cardAsset.highHeal.enabled)
        {
            gameManager.GetComponent<GameManager>().HealAbility(cardAsset.highHeal.effect, cardAsset.highHeal.cost, this.gameObject, cardAsset.highHeal.used);
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
    }

    public void DamageAbility()
    {
        if (cardAsset.lowDMG.enabled)
        {
            gameManager.GetComponent<GameManager>().DMGAbility(cardAsset.lowDMG.effect, cardAsset.lowDMG.cost, this.gameObject, cardAsset.lowDMG.used);
        }
        else if (cardAsset.highDMG.enabled)
        {
            gameManager.GetComponent<GameManager>().DMGAbility(cardAsset.highDMG.effect, cardAsset.highDMG.cost, this.gameObject, cardAsset.highDMG.used);
        }
    }
    public void UsedDamage()
    {
        cardAsset.lowDMG.used = true;
        cardAsset.highDMG.used = true;
    }

    public void Damage(int damage)
    {
        Health = Health - damage;
    }
    #endregion
}
