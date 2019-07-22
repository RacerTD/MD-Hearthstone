using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanCardManager : MonoBehaviour
{
    public HumanCardAsset cardAsset;

    [Header("CardComponents")]
    public Image cardGraphic;
    public Text titleText;
    public Text price;
    public Text attack;
    public Text life;


    void Start()
    {
        UpdateCard();
    }

    void UpdateCard(HumanCardAsset newAsset = null)
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
        cardGraphic.sprite = cardAsset.cardImage;

    }
}
