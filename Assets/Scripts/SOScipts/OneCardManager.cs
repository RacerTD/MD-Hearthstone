using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneCardManager : MonoBehaviour
{
    public CardAsset cardAsset;

    [Header("CardComponents")]
    public Image cardGraphic;
    public Text titleText;
    public Text price;
    public Text attack;
    public Text life;

    [Header("Kartenart Objekte")]
    public GameObject enemyCardFront;
    public GameObject enemyCardBack;
    public GameObject spellCardFront;
    public GameObject spellCardBack;
    public GameObject equipmentCardFront;
    public GameObject equipmentCardBack;
    public GameObject humanCardFront;
    public GameObject humanCardBack;

    void Start()
    {
        UpdateCard();
    }

    void UpdateCard(CardAsset newAsset = null)
    {
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
            enemyCardFront.SetActive;
        }

    }
}
