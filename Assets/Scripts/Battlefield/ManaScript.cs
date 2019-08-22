using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    public GameManager gameManager;
    public int manaCount = 0;
    public int maxMana = 0;
    int childcounter;

    void Update()
    {
        if (childcounter != transform.childCount)
        {
            UpdateMana();
        }
        childcounter = transform.childCount;
    }

    public void UpdateMana()
    {
        if (gameManager.humanKilled == false)
        {
            foreach (Transform child in transform)
            {
                manaCount = manaCount + child.GetComponent<OneCardManager>().cost;
                maxMana = maxMana + child.GetComponent<OneCardManager>().cost;
                gameManager.humanKilled = true;
                child.GetComponent<OneCardManager>().delete();
            }
            gameManager.GetComponent<HudManager>().UpdateMana(manaCount);
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<OneCardManager>().BackToHand();
            }
        }
    }

    public void UsedMana(int cost)
    {
        manaCount = manaCount - cost;
        gameManager.GetComponent<HudManager>().UpdateMana(manaCount);
    }

    public void TurnBegin()
    {
        manaCount = maxMana;
        gameManager.GetComponent<HudManager>().UpdateMana(manaCount);
    }
}
