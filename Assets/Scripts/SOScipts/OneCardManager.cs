using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OneCardManager : MonoBehaviour
{
    public GameObject enemyField;
    public GameObject gameManger;
    public GameObject cardDeck;
    public CardAsset cardAsset;
    public string prefabName;

    [Header("CardComponents")]
    public Image cardGraphic;
    public List<Text> costText = new List<Text>();
    public List<Text> nameText = new List<Text>();
    public List<Text> descriptionText = new List<Text>();
    public List<Text> attackText = new List<Text>();
    public List<Text> lifeText = new List<Text>();
    public List<Text> maxLifeText = new List<Text>();

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject spellCardFront;
    public GameObject equipmentCardFront;
    public GameObject humanCardFront;
    public GameObject boardCard;
    //public GameObject CardBack;

    

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
    public int attack;

    public int cost;

    private int equipmentCount;
    //private bool hasAttacked = false;
    //private bool summoningSickness = true;

    private void Awake()
    {
        gameManger = GameObject.Find("GameManager");
        enemyField = GameObject.Find("EnemyField");
        cardDeck = GameObject.Find("CardDeck");
        if (gameManger.GetComponent<GameManager>().gameState != GameManager.GameState.Enemy)
        {
            cardAsset = cardDeck.GetComponent<CardDeckScript>().CardToSpawn();
        }
        else
        {
            cardAsset = enemyField.GetComponent<EnemyFieldScript>().cardToSpawn();
        }
        InitializeCard();
    }
    private void Start()
    {

    }

    private void Update()
    {

    }

    void InitializeCard(CardAsset newAsset = null)
    {
        if (gameManger.GetComponent<GameManager>().gameState != GameManager.GameState.Enemy)
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

        attack = newAsset.attack;
        maxHealth = newAsset.maxHealth;
        Health = maxHealth;
        cost = newAsset.cost;


        UpdateList(costText, cost.ToString());
        UpdateList(nameText, cardAsset.name);
        UpdateList(descriptionText, cardAsset.description);
        UpdateList(attackText, attack.ToString());
        UpdateList(lifeText, _health.ToString());
        UpdateList(maxLifeText, maxHealth.ToString());

    }

    public void delete()
    {
        Destroy(gameObject);
    }

    public void GiveGameManagerCard()
    {
        gameManger.GetComponent<GameManager>().CardClicked(gameObject);
    }

    private void UpdateList(List<Text> bla, string value)
    {
        for (int i = 0; i <= bla.Count - 1; i++)
        {
            bla[i].text = value;
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
        humanCardFront.SetActive(false);
        boardCard.SetActive(true);
    }

    public void HealAbility()
    {
        if (cardAsset.lowHeal.enabled)
        {
            gameManger.GetComponent<GameManager>().HealAbility(cardAsset.lowHeal.effect, cardAsset.lowHeal.cost);
        }
        else if (cardAsset.highHeal.enabled)
        {
            gameManger.GetComponent<GameManager>().HealAbility(cardAsset.highHeal.effect, cardAsset.highHeal.cost);
        }
        
    }
}
