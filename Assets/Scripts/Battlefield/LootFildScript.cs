using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFildScript : MonoBehaviour
{

    public List<GameObject> possibleLoot = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnAbility()
    {
        Instantiate(possibleLoot[Random.Range(0, possibleLoot.Count)], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void OnEndTurn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<LootScript>().Destroy();
        }
    }

}
