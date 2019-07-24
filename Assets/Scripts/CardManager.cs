using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{   
    public GameObject entityToSpawn;
    public OneCardManager spawnManagerValues;
    void Start()
    {
        SpawnCards();
    }
    void SpawnCards()
    {
        Vector3 test = new Vector3(0f, 0f, 0f);

        GameObject currentEntity = Instantiate(entityToSpawn, test, Quaternion.identity);
        currentEntity.name = spawnManagerValues.name;
    }
}
