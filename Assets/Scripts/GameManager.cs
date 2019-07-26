using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public GameObject hand;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cardDeck.GetComponent<CardDeckScript>().moveCardToHand();
        }
        if (Input.GetKeyDown("c"))
        {
            hand.GetComponent<HandScript>().moveCardToManPower();
        }
        if (Input.GetKeyDown("v"))
        {
            hand.GetComponent<HandScript>().moveCardToMana();
        }
    }
}
