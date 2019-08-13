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

        if (gameManager.highlight != Highlight.Nothing)
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
}
