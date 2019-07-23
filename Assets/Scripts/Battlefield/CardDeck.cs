using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<CardAsset> deckCards = new List<CardAsset>();
    public CardAsset cardAsset;

    public GameObject cardToSpawn;
    public CardAsset cardValues;
    void Start()
    {
        SpawnCards();
    }

    // Update is called once per frame
    void SpawnCards()
    {
        Vector3 cardPos = new Vector3(0, 0, 0);
        GameObject currentCard = Instantiate(cardToSpawn, cardPos, Quaternion.identity);
    }
}
