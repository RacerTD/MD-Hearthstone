using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManPowerScript : MonoBehaviour
{
    int manPower = 0;
    public Text manPowerText;
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        manPowerText.text = manPower.ToString();
    }

    public void UpdateManPower()
    {
        manPower = 0;
        foreach (Transform child in transform)
        {
            manPower = manPower + child.GetComponent<OneCardManager>().cost;
        }
    }
}
