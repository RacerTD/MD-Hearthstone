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
    public List<Text> costText = new List<Text>();
    public List<Text> nameText = new List<Text>();
    public List<Text> descriptionText = new List<Text>();
    public int cost;
    public Text attackText;
    public Text lifeText;
    public Text maxLifeText;

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject spellCardFront;
    public GameObject equipmentCardFront;
    public GameObject humanCardFront;
    public GameObject CardBack;

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
        cost = cardAsset.cost;
        transform.SetParent(cardDeck.transform, false);
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

    public void HandPosition(int i)
    {
        transform.position = new Vector3(i, i, 0);
    }
}
