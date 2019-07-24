using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckScript : MonoBehaviour
{
    public List<CardAsset> deckCards = new List<CardAsset>();
    public GameObject cardPrefab;

    void Start()
    {
        spawnCardDeck();
    }

    void Update()
    {
        
    }

    void spawnCardDeck()
    {
        int i = deckCards.Count;
        do
        {
            Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            i--;
        } while (i > 0);
        
    }
    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = deckCards[0];
        deckCards.RemoveAt(0);
        return cardToSpawn;
    }
}
