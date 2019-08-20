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
    public ManaScript mana;
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
            childCount = transform.childCount;
            for (int i = childCount - 1; i > -1; i--)
            {
                if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cardType == CardType.AOEHealSpell)
                {
                    AOEHeal(transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cost, transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.attack);
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            UpdateCardParts();
        }
        

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

    private void AOEHeal(int cost, int effect)
    {
        mana.UsedMana(cost);
        for (int k = transform.childCount - 1; k >= 0; k--)
        {
            transform.GetChild(k).GetComponent<OneCardManager>().Heal(effect);
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
        Debug.Log("Player Turn Begin");
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
