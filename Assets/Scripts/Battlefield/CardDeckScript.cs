using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckScript : MonoBehaviour
{
    public List<CardAsset> deckCards = new List<CardAsset>();
    public GameObject hand;
    public GameObject cardPrefab;

    void Start()
    {
        shuffleDeck();
        spawnCardDeck();
    }

    void shuffleDeck()
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
    public void moveCardToHand()
    {
        this.gameObject.transform.GetChild(0).SetParent(hand.transform, false);
    }
}
