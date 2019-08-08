using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    public TextMeshProUGUI manPowerText;
    public TextMeshProUGUI manaText;

    

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
