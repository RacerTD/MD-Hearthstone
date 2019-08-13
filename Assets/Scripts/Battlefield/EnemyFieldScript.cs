using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// Contains Logic of Enemy turn
/// </summary>
public class EnemyFieldScript : MonoBehaviour
{
    [Header("Gegner die Gespawnt werden können")]
    public List<CardAsset> hardEnemyCards = new List<CardAsset>();
    public List<CardAsset> softEnemyCards = new List<CardAsset>();
    public CardAsset queen;

    [Header("Wichtige Dinge vom Feld")]
    public GameObject cardPrefab;
    public GameManager gameManager;
    public PlayerFieldScript playerField;

    [Header("Anderes")]
    public float timeBetweenActions = 5;

    [Header("Variablen für andere Scripte")]
    public bool taunt = false;

    private Transform myChild;
    private int childCount;
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
                    TurnStart();
                    EnemyCardSpawn();
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

    private void TurnStart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().TurnBegin();
        }
    }

    private void EnemyCardSpawn()
    {

    }

    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = hardEnemyCards[Random.Range(0, hardEnemyCards.Count)];
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
