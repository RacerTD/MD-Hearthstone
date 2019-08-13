using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EnemyFieldScript : MonoBehaviour
{
    public List<CardAsset> enemyCards = new List<CardAsset>();
    public GameObject cardPrefab;
    public GameManager gameManager;
    public PlayerFieldScript playerField;
    private Transform myChild;
    public bool taunt = false;
    private int childCount;
    public float timeBetweenActions = 5;
    private float timer;
    private int childNumber;

    private EnemyState enemyState = EnemyState.Start;

    private enum EnemyState
    {
        Start,
        Attack,
        Wait,
        End
    }


    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (childCount != transform.childCount)
        {
            HasTaunt();
            //Debug.Log("Baum");
        }
        childCount = transform.childCount;


        if (gameManager.gameState == GameState.Enemy)
        {
            //Alles was der Gegner machet.
            switch (enemyState)
            {
                case EnemyState.Start:
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Wait:
                    break;
                case EnemyState.End:
                    break;
            }
        }
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

    private void AttackSomeone()
    {

    }
}
