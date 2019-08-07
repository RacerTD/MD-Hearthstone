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
    void Start()
    {
        
    }

    // Update is called once per frame
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
        foreach (Transform child in transform)
        {
            manaCount = manaCount + child.GetComponent<OneCardManager>().cost;
            maxMana = maxMana + child.GetComponent<OneCardManager>().cost;
            child.GetComponent<OneCardManager>().delete();
        }
        gameManager.GetComponent<HudManager>().UpdateMana(manaCount);
    }

    public void UsedMana(int cost)
    {
        manaCount = manaCount - cost;
        gameManager.GetComponent<HudManager>().UpdateMana(manaCount);
    }
}
