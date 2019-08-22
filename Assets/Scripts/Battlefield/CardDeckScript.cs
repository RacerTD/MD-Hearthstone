using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDeckScript : MonoBehaviour
{
    public List<CardAsset> deckCards = new List<CardAsset>();
    public HandScript hand;
    public GameObject cardPrefab;
    public List<CardAsset> startCards = new List<CardAsset>();
    public List<CardAsset> spellCards = new List<CardAsset>();

    public void ShuffleDeck()
    {
        CardAsset merker;
        int switcher;
        int spott1;
        int spott2;
        for (int i = 0; i <= (deckCards.Count - 1); i++)
        {
            spott1 = Random.Range(0, deckCards.Count);
            spott2 = Random.Range(0, deckCards.Count);

            merker = deckCards[spott1];
            deckCards[spott1] = deckCards[spott2];
            deckCards[spott2] = merker;
        }
    }

    public void SpawnCardDeck()
    {
        int i = deckCards.Count;
        do
        {
            Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
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
        this.gameObject.transform.GetChild(0).SetParent(hand.transform, true);
    }

    public void SpawnSpellCard()
    {
        Instantiate(cardPrefab, hand.transform);
    }
}
