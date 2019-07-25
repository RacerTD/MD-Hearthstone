using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{   
    public GameObject entityToSpawn;
    public OneCardManager spawnManagerValues;
    void Start()
    {

    }
    
    void checkHandCards()
    {
        int handCards = this.gameObject.transform.childCount;
    }
}
