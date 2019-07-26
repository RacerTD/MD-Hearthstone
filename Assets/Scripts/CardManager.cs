using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject playerField;
    public GameObject manPower;
    public GameObject mana;
    void Start()
    {

    }
    public void moveCardToField()
    {
        this.gameObject.transform.GetChild(0).SetParent(playerField.transform, false);
    }
    public void moveCardToMana()
    {
        this.gameObject.transform.GetChild(0).SetParent(mana.transform, false);
    }
    public void moveCardToManPower()
    {
        this.gameObject.transform.GetChild(0).SetParent(manPower.transform, false);
    }
}
