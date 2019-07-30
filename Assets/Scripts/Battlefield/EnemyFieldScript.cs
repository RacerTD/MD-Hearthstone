﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldScript : MonoBehaviour
{
    public List<CardAsset> enemyCards = new List<CardAsset>();
    public GameObject cardPrefab;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = enemyCards[0];
        enemyCards.RemoveAt(0);
        return cardToSpawn;
    }
    public void SpawnNewEnemy()
    {
        Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}