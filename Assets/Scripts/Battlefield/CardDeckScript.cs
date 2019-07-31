﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckScript : MonoBehaviour
{
    public List<CardAsset> deckCards = new List<CardAsset>();
    public GameObject hand;
    public GameObject cardPrefab;

    void Start()
    {
        
    }

    public void ShuffleDeck()
    {
        CardAsset merker;
        int switcher;
        for (int i = 0; i <= (deckCards.Count - 1); i++)
        {
            merker = deckCards[i];
            switcher = Random.Range(0, deckCards.Count);
            deckCards[i] = deckCards[switcher];
            deckCards[switcher] = merker;
        }
    }

    public void SpawnCardDeck()
    {
        int i = deckCards.Count;
        do
        {
            Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            i--;
        } while (i > 0);
        
    }
    public CardAsset CardToSpawn()
    {
        CardAsset cardToSpawn = deckCards[0];
        deckCards.RemoveAt(0);
        return cardToSpawn;
    }
    public void MoveCardToHand()
    {
        this.gameObject.transform.GetChild(0).SetParent(hand.transform, false);
    }
}
