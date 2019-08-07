using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text manPowerText;
    public Text manaText;

    

    void Update()
    {
        
    }

    public void UpdateManPower(int manPower)
    {
        manPowerText.text = manPower.ToString();
    }

    public void UpdateMana(int mana)
    {
        manaText.text = mana.ToString();
    }
}
