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
    public List<CardAsset> strongEnemyCards = new List<CardAsset>();
    public List<CardAsset> weakEnemyCards = new List<CardAsset>();
    public List<CardAsset> eggCard = new List<CardAsset>();
    public CardAsset queen;
    public List<CardAsset> cardsToSpawn = new List<CardAsset>();

    [Header("Wichtige Dinge vom Feld")]
    public GameObject cardPrefab;
    public GameManager gameManager;
    public PlayerFieldScript playerField;

    [Header("Anderes")]
    public float timeBetweenActions = 5;

    [Header("Variablen für andere Scripte")]
    public int enemyWaveCount = 0;
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
                    AttackSomeone();
                    break;
                case EnemyState.Wait:
                    break;
                case EnemyState.End:
                    gameManager.gameState = GameState.PlayerCardDraw;
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
        if (transform.childCount == 0)
        {
            switch (enemyWaveCount)
            {
                case 0:
                    SpawnEggEnemy();
                    SpawnWeakEnemy();
                    SpawnWeakEnemy();
                    enemyState = EnemyState.End;
                    break;
                case 1:
                    SpawnWeakEnemy();
                    SpawnStrongEnemy();
                    SpawnEggEnemy();
                    break;
                case 2:
                    SpawnStrongEnemy();
                    SpawnStrongEnemy();
                    SpawnEggEnemy();
                    break;
                case 3:
                    break;
            }
            enemyWaveCount++;
            enemyState = EnemyState.End;
        }
    }

    private void SpawnEggEnemy()
    {
        cardsToSpawn.Add(eggCard[Random.Range(0, eggCard.Count)]);
        Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    private void SpawnWeakEnemy()
    {
        cardsToSpawn.Add(weakEnemyCards[Random.Range(0, weakEnemyCards.Count)]);
        Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    private void SpawnStrongEnemy()
    {
        cardsToSpawn.Add(strongEnemyCards[Random.Range(0, strongEnemyCards.Count)]);
        Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = cardsToSpawn[0];
        cardsToSpawn.RemoveAt(0);

        return cardToSpawn;
    }
    /// <summary>
    /// Spawns the needed Enemys for the round.
    /// </summary>
    private void SpawnNewEnemy()
    {
        
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
