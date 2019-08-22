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
    public CardDeckScript cardDeck;
    public ManaScript mana;
    public HandScript hand;

    [Header("Anderes")]
    public float timeBetweenActions = 5;

    [Header("Variablen für andere Scripte")]
    public int enemyWaveCount = -1;

    private int gameRound = 0;

    private Transform myChild;
    private int childCount;
    private float timer;
    private bool attacked = false;
    private bool firstTurn = true;
    private bool firstTurn_ = true;
    private int timeToWait = 2;

    [Header("Werte für Queen")]
    public int queenHealth;
    public int queenAttack;

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
            Debug.Log("Called");
            childCount = transform.childCount;
            for (int i = childCount - 1; i > -1; i--)
            {
                transform.GetChild(i).GetComponent<Draggable>().enabled = false;
                if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cardType == CardType.AOEDMGSpell)
                {
                    if (transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cost <= mana.manaCount)
                    {
                        AOEDamage(transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.cost, transform.GetChild(i).GetComponent<OneCardManager>().cardAsset.attack);
                        Destroy(transform.GetChild(i).gameObject);
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<OneCardManager>().BackToHand();
                    }
                }
            }
        }

        if (gameManager.gameState == GameState.Enemy)
        {
            switch (enemyState)
            {
                case EnemyState.Start:
                    gameManager.ResetAbilitys();
                    TurnStart();
                    hand.DeactivateDraggingScript();
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
                    
                    gameManager.gameState = GameState.PlayerCardDraw;
                    break;
            }
        }
    }

    private void AOEDamage(int cost, int effect)
    {
        mana.UsedMana(cost);
        for (int k = transform.childCount - 1; k >= 0; k--)
        {
            transform.GetChild(k).GetComponent<OneCardManager>().Damage(effect);
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
        gameRound++;
        if (transform.childCount == 0)
        {
            enemyWaveCount++;
            if (enemyWaveCount != 0)
            {
                cardDeck.SpawnSpellCard();
            }
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
                CalcQueenStats();
                SpawnEggEnemy();
                SpawnStrongEnemy();
                SpawnQueenEnemy();
                SpawnStrongEnemy();
                SpawnEggEnemy();
                enemyWaveCount++;
                break;
            case 4:
                SpawnEggEnemy();
                SpawnStrongEnemy();
                SpawnStrongEnemy();
                SpawnEggEnemy();
                break;
            default:
                break;
        }
        timer = 0;
    }

    private void CalcQueenStats()
    {
        queenHealth = 10 + gameRound * 2;
        queenAttack = 2 + Mathf.FloorToInt(gameRound * 0.5f);
    }


    private void SpawnEggEnemy()
    {
        if (transform.childCount < 7)
        {
            cardsToSpawn.Add(eggCard[Random.Range(0, eggCard.Count)]);
            Instantiate(cardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
    }
    private void SpawnQueenEnemy()
    {
        if (transform.childCount < 7)
        {
            cardsToSpawn.Add(queen);
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
            }
        }
        else
        {
            gameManager.TriggerEndScreen(true);
            enemyState = EnemyState.End;
        }
    }

   
    public void ResetEnemyState()
    {
        enemyState = EnemyState.Start;
    }

    public void ActivateSummoningSicness() { 
    
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<OneCardManager>().summoningSickness = true;
        }
    }
}
