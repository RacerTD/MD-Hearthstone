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
    private List<OneCardManager> attackList = new List<OneCardManager>();

    [Header("Wichtige Dinge vom Feld")]
    public GameObject cardPrefab;
    public GameManager gameManager;
    public PlayerFieldScript playerField;

    [Header("Anderes")]
    public float timeBetweenActions = 5;

    [Header("Variablen für andere Scripte")]
    public int enemyWaveCount = -1;

    private Transform myChild;
    private int childCount;
    private float timer;
    private bool attacked = false;
    private bool firstTurn = true;
    private bool firstTurn_ = true;
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
            switch (enemyState)
            {
                case EnemyState.Start:
                    gameManager.ResetAbilitys();
                    TurnStart();
                    EnemyCardSpawn();

                    if (firstTurn)
                    {
                        enemyState = EnemyState.End;
                        firstTurn = false;
                    }
                    else
                    {
                        GenerateAttackList();
                        enemyState = EnemyState.Wait;
                    }

                    break;

                case EnemyState.Attack:
                    if (attackList.Count != 0)
                    {
                        NewAttack();
                    }
                    else
                    {
                        enemyState = EnemyState.End;
                    }
                    break;

                case EnemyState.Wait:
                    if (Wait(timeToWait))
                    {
                        enemyState = EnemyState.Attack;
                    }
                    break;

                case EnemyState.End:
                    if (firstTurn_)
                    {
                        firstTurn_ = false;
                    }
                    else
                    {
                        //TurnStart();
                    }
                    
                    gameManager.gameState = GameState.PlayerCardDraw;
                    break;
            }
        }
    }

    private void GenerateAttackList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy && transform.GetChild(i).GetComponent<OneCardManager>().summoningSickness == false)
            {
                attackList.Add(transform.GetChild(i).GetComponent<OneCardManager>());
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

    public void TurnStart()
    {
        Debug.Log("Enemy Turn Begin");
        for (int i = 0; i < transform.childCount; i++)
        {
            myChild = transform.GetChild(i);
            myChild.GetComponent<OneCardManager>().TurnBegin();
            myChild.GetComponent<OneCardManager>().DeactivateSummoningSickness();
        }
        attacked = false;
    }

    private void EnemyCardSpawn()
    {
        if (transform.childCount == 0)
        {
            enemyWaveCount++;
            Debug.Log("Wave Count +1 " + enemyWaveCount);
        }

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
        timer = 0;
    }

    private void SpawnEggEnemy()
    {
        if (transform.childCount < 7)
        {
            cardsToSpawn.Add(eggCard[Random.Range(0, eggCard.Count)]);
            Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
        
    }

    private void SpawnWeakEnemy()
    {
        if (transform.childCount < 7)
        {
            cardsToSpawn.Add(weakEnemyCards[Random.Range(0, weakEnemyCards.Count)]);
            Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
    }

    private void SpawnStrongEnemy()
    {
        if (transform.childCount < 7)
        {
            cardsToSpawn.Add(strongEnemyCards[Random.Range(0, strongEnemyCards.Count)]);
            Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
    }

    public CardAsset cardToSpawn()
    {
        CardAsset cardToSpawn = cardsToSpawn[0];
        cardsToSpawn.RemoveAt(0);

        return cardToSpawn;
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

    private void NewAttack()
    {
        if (playerField.transform.childCount > 0)
        {
            if (playerField.HasTaunt())
            {
                do
                {
                    int merker = Random.Range(0, playerField.GetComponent<Transform>().childCount);
                    if (playerField.GetComponent<Transform>().GetChild(merker).GetComponent<OneCardManager>().cardAsset.taunt)
                    {
                        attackList[0].GiveGameManagerCard();
                        attackList.RemoveAt(0);
                        playerField.GetComponent<Transform>().GetChild(merker).GetComponent<OneCardManager>().GiveGameManagerCard();
                        attacked = true;
                        timer = 0;
                        enemyState = EnemyState.Wait;
                        Debug.Log("Attacked");
                    }
                } while (!attacked);
            }
            else
            {
                attackList[0].GiveGameManagerCard();
                attackList.RemoveAt(0);
                playerField.GetComponent<Transform>().GetChild(Random.Range(0, playerField.GetComponent<Transform>().childCount)).GetComponent<OneCardManager>().GiveGameManagerCard();
                attacked = true;
                timer = 0;
                enemyState = EnemyState.Wait;
                Debug.Log("Attacked");
            }
        }
        else
        {
            //Deathscreen
            enemyState = EnemyState.End;
        }
    }

   
    public void ResetEnemyState()
    {
        enemyState = EnemyState.Start;
    }

    public void ActivateSummoningSicness()
    {
        Debug.Log("Activated Summoning Sicness");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<OneCardManager>().summoningSickness = true;
        }
    }
}
