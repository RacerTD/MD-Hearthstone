using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManPowerScript : MonoBehaviour
{
    public int manPower = 0;
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
            child.GetComponent<OneCardManager>().delete();
        }
    }
}
