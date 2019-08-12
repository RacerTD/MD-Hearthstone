using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldScript : MonoBehaviour
{
    public List<CardAsset> enemyCards = new List<CardAsset>();
    public GameObject cardPrefab;
    public GameManager gameManager;
    public Transform myChild;
    public bool taunt = false;
    private int childCount;
    void Start()
    {
        
    }

    void Update()
    {
        if (childCount != transform.childCount)
        {
            HasTaunt();
            //Debug.Log("Baum");
        }
        childCount = transform.childCount;
    }

    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = enemyCards[Random.Range(0, enemyCards.Count)];
        return cardToSpawn;
    }

    public void SpawnNewEnemy()
    {
        Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    public void HasTaunt()
    {
        taunt = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            if (myChild.GetComponent<OneCardManager>().cardAsset.taunt)
            {
                taunt = true;
            }
        }
    }
}
