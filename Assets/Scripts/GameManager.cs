using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public GameObject hand;
    public GameObject enemyField;
    public bool playersTurn;
    public enum GameState
    {
        Enemy,
        PlayerCardDraw,
        PlayerIdle,
        PlayerCardInHand,
        PlayerAttack,
        PlayerAbility
    }

    public GameState gameState;

    IEnumerator EnemyState()
    {
        while (gameState == GameState.Enemy)
        {
            yield return 0;
        }
    }
    IEnumerator PlayerCardDrawState()
    {
        while (gameState == GameState.PlayerCardDraw)
        {
            yield return 0;
        }
    }
    IEnumerator PlayerIdleState()
    {
        while (gameState == GameState.PlayerIdle)
        {
            yield return 0;
        }
    }
    IEnumerator PlayerCardInHandState()
    {
        while (gameState == GameState.PlayerCardInHand)
        {
            yield return 0;
        }
    }
    IEnumerator PlayerAttackState()
    {
        while (gameState == GameState.PlayerAttack)
        {
            yield return 0;
        }
    }
    IEnumerator PlayerAbilityState()
    {
        while (gameState == GameState.PlayerAbility)
        {
            yield return 0;
        }
    }




    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cardDeck.GetComponent<CardDeckScript>().moveCardToHand();
        }
        if (Input.GetKeyDown("x"))
        {
            enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
        }
        if (Input.GetKeyDown("c"))
        {
            playersTurn = !playersTurn;
        }
    }

    void BaseAttack()
    {

    }
}
