using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneCardManager : MonoBehaviour
{
    private GameManager gameManager;
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

    private void Awake()
    {
        
    }
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
