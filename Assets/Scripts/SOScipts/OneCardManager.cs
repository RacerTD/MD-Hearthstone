using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OneCardManager : MonoBehaviour
{
    public GameObject cardDeck;
    public CardAsset cardAsset;
    public string prefabName;

    [Header("CardComponents")]
    public Image cardGraphic;
    public Text titleText;
    public Text price;
    public Text attack;
    public Text life;

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject spellCardFront;
    public GameObject equipmentCardFront;
    public GameObject humanCardFront;
    public GameObject CardBack;

    [HideInInspector]
    public int cost;

    private void Awake()
    {
        cardDeck = GameObject.Find("CardDeck");
    }
    void Start()
    {
        cardAsset = cardDeck.GetComponent<CardDeckScript>().cardToSpawn();
        UpdateCard();
    }

    void UpdateCard(CardAsset newAsset = null)
    {
        tag = "CardDeck";
        transform.SetParent(cardDeck.transform, false);
        cost = cardAsset.cost;
        if (newAsset == null && cardAsset == null)
        {
            return;
        }

        if (newAsset == null)
        {
            newAsset = cardAsset;
        }

        titleText.text = cardAsset.name;
        cardGraphic.sprite = cardAsset.cardImageLarge;
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


    }
}
