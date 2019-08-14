using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class PlayerFieldScript : MonoBehaviour
{
    int childCount = 0;
    Transform myChild;
    public GameManager gameManager;
    public GameObject manPower;
    private CardType currentHandCard;

    public Image toHighlight;
    public Color humanCardHighlight;
    public Color defaultColor;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (childCount != transform.childCount)
        {
            UpdateCardParts();
        }
        childCount = transform.childCount;

        if (currentHandCard != gameManager.cardInHand)
        {
            currentHandCard = gameManager.cardInHand;

            if (currentHandCard == CardType.Human && gameManager.currentlyDragging.cardAsset.cost <= manPower.GetComponent<ManPowerScript>().manPower)
            {
                toHighlight.color = humanCardHighlight;
            }
            else
            {
                toHighlight.color = defaultColor;
            }
        }
    }

    void UpdateCardParts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().NowOnField();
        }
    }

    public void TurnBegin()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().TurnBegin();
        }
    }

    public bool HasTaunt()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.taunt)
            {
                return true;
            }
        }
        return false;
    }
}
