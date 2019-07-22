using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCardManager : MonoBehaviour
{
    public SpellCardAsset cardAsset;

    [Header("CardComponents")]
    public Image cardGraphic;
    public Text titleText;
    public Text price;


    void Start()
    {
        UpdateCard();
    }

    void UpdateCard(SpellCardAsset newAsset = null)
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
