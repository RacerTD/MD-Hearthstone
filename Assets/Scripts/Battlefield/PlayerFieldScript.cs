using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFieldScript : MonoBehaviour
{
    int childCount = 0;
    Transform myChild;
    public GameObject gameManager;
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

        if (currentHandCard != gameManager.GetComponent<GameManager>().cardInHand)
        {
            currentHandCard = gameManager.GetComponent<GameManager>().cardInHand;

            if (currentHandCard == CardType.Human && gameManager.GetComponent<GameManager>().currentlyDragging.GetComponent<OneCardManager>().cardAsset.cost <= manPower.GetComponent<ManPowerScript>().manPower)
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
            //myChild.GetComponent<Draggable>().enabled = false;
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
}
