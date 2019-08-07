using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManPowerScript : MonoBehaviour
{
    public GameManager gameManager;
    public int manPower = 0;
    public int maxManPower = 0;
    int childcounter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (childcounter != transform.childCount)
        {
            UpdateManPower();
        }
        childcounter = transform.childCount;
    }

    public void UpdateManPower()
    {
        foreach (Transform child in transform)
        {
            manPower = manPower + child.GetComponent<OneCardManager>().cost;
            maxManPower = manPower + child.GetComponent<OneCardManager>().cost;

            child.GetComponent<OneCardManager>().delete();
        }
        gameManager.GetComponent<HudManager>().UpdateManPower(manPower);
    }

    public void UsedManPower(int mp)
    {
        manPower = manPower - mp;
        gameManager.GetComponent<HudManager>().UpdateManPower(manPower);
    }
}
