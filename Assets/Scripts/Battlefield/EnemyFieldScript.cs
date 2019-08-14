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

    private Transform myChild;
    private int childCount;
    private float timer;
    private int childNumber = 0;
    private bool attacked = false;
    private bool firstTurn = true;
    private int attackNumber = 0;
    private int timeToWait = 2;

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


        if (gameManager.gameState == GameState.Enemy)
        {
            //Alles was der Gegner machet.
            switch (enemyState)
            {
                case EnemyState.Start:
                    TurnStart();
                    EnemyCardSpawn();
                    

                    if (firstTurn)
                    {
                        enemyState = EnemyState.End;
                        firstTurn = false;
                    }
                    else
                    {
                        enemyState = EnemyState.Wait;
                    }

                    break;

                case EnemyState.Attack:
                    InitiateAttack();
                    break;

                case EnemyState.Wait:
                    if (Wait(timeToWait))
                    {
                        enemyState = EnemyState.Attack;
                    }
                    break;

                case EnemyState.End:
                    gameManager.gameState = GameState.PlayerCardDraw;
                    break;
            }
        }
    }

    private bool Wait(float waitTime)
    {
        if (timer >= waitTime)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void TurnStart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().TurnBegin();
        }

        childNumber = 0;
        attackNumber = 0;
        attacked = false;
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
                    enemyState = EnemyState.Wait;
                    break;
                case 1:
                    SpawnWeakEnemy();
                    SpawnStrongEnemy();
                    SpawnEggEnemy();
                    enemyState = EnemyState.Wait;
                    break;
                case 2:
                    SpawnStrongEnemy();
                    SpawnStrongEnemy();
                    SpawnEggEnemy();
                    enemyState = EnemyState.Wait;
                    break;
                case 3:
                    break;
            }
            enemyWaveCount++;

            
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

    public bool HasTaunt()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            if (myChild.GetComponent<OneCardManager>().cardAsset.taunt)
            {
                return true;
            }
        }
        return false;
    }

    private void InitiateAttack()
    {
        do
        {
            if (CanDoSomething())
            {
                Attack();
            }
            else
            {
                attacked = true;
                gameManager.gameState = GameState.PlayerCardDraw;
            }
        } while (!attacked);

        attacked = false;
        timer = 0f;
    }

    private void Attack()
    {
        if (transform.GetChild(attackNumber).GetComponent<OneCardManager>().cardAsset.cardType != CardType.Egg)
        {
            if (playerField.HasTaunt())
            {
                do
                {
                    int merker = Random.Range(0, playerField.GetComponent<Transform>().childCount);
                    if (playerField.GetComponent<Transform>().GetChild(merker).GetComponent<OneCardManager>().cardAsset.taunt)
                    {
                        transform.GetChild(attackNumber).GetComponent<OneCardManager>().GiveGameManagerCard();
                        playerField.GetComponent<Transform>().GetChild(merker).GetComponent<OneCardManager>().GiveGameManagerCard();
                        attacked = true;
                        enemyState = EnemyState.Wait;
                        Debug.Log("Attacked");
                    }
                } while (!attacked);
            }
            else
            {
                transform.GetChild(attackNumber).GetComponent<OneCardManager>().GiveGameManagerCard();
                playerField.GetComponent<Transform>().GetChild(Random.Range(0, playerField.GetComponent<Transform>().childCount)).GetComponent<OneCardManager>().GiveGameManagerCard();
                attacked = true;
                enemyState = EnemyState.Wait;
                Debug.Log("Attacked");
            }
        }
    }

    public void ResetEnemyState()
    {
        enemyState = EnemyState.Start;
    }

    private bool CanDoSomething()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy && !transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.attackUsed)
            {
                attackNumber = i;
                return true;
            }
        }
        return false;
    }
}
