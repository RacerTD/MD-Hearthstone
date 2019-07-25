using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    int manaCount = 0;
    int maxMana = 0;
    public Text manaText;
    public Text maxManaText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        manaText.text = manaCount.ToString();
        maxManaText.text = maxMana.ToString();
    }

    public void updateManaText()
    {
        manaCount = 0;
        foreach (Transform child in transform)
        {
            manaCount = manaCount + child.GetComponent<OneCardManager>().cost;
        }
        maxMana = manaCount;
    }
}
