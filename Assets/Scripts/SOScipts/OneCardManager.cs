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
    public GameObject CardBack;

    [Header("Karten Attribute")]
    public string cardType;
    public int health;
    public int maxHealth;
    public int attack;

    public int cost;

    public int equipmentCount;
    public bool hasAttacked = false;
    public bool summoningSickness = true;

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
    void Start()
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

        attack = cardAsset.attack;
        maxHealth = cardAsset.maxHealth;
        health = maxHealth;
        cardType = cardAsset.cardType;
        cost = cardAsset.cost;

        if (newAsset == null && cardAsset == null)
        {
            return;
        }

        if (newAsset == null)
        {
            newAsset = cardAsset;
        }

        if (newAsset.cardType == "enemy")
        {
            enemyCardFront.SetActive(true);
        }
        else if (newAsset.cardType == "spell")
        {
            spellCardFront.SetActive(true);
        }
        else if (newAsset.cardType == "equipment")
        {
            equipmentCardFront.SetActive(true);
        }
        else
        {
            humanCardFront.SetActive(true);
        }
        
        for (int i = 0; i >= costText.Count; i++)
        {
            costText[i].text = cardAsset.cost.ToString();
        }
        for (int i = 0; i >= nameText.Count; i++)
        {
            nameText[i].text = cardAsset.name.ToString();
        }
        for (int i = 0; i >= descriptionText.Count; i++)
        {
            descriptionText[i].text = cardAsset.description.ToString();
        }

    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
