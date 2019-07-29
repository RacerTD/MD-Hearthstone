using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public GameObject playerField;
    public GameObject manPower;
    public GameObject mana;

    public int Handcards
    {
        get { return _handcards; }
        set { _handcards = value; }
    }

    private int _handcards;

    public List<GameObject> handPositions = new List<GameObject>();
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
        mana.GetComponent<ManaScript>().updateManaText();
    }
    public void moveCardToManPower()
    {
        this.gameObject.transform.GetChild(0).SetParent(manPower.transform, false);
        manPower.GetComponent<ManPowerScript>().UpdateManPower();
    }
}
