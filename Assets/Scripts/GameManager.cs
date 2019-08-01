using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardDeck;
    public GameObject hand;
    public GameObject enemyField;
    public bool playersTurn;

    public GameObject clicked01 = null;
    public GameObject clicked02 = null;
    
    public enum GameState
    {
        GameStart,
        Enemy,
        PlayerCardDraw,
        PlayerIdle,
        PlayerCardInHand,
        PlayerAttack,
        PlayerAbility
    }

    public GameState gameState;

    void Awake()
    {

    }
    private void Start()
    {
        gameState = GameState.GameStart;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cardDeck.GetComponent<CardDeckScript>().MoveCardToHand();
        }
        if (Input.GetKeyDown("x"))
        {
            enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
        }


        switch (gameState)
        {
            case GameState.GameStart:
                //KartenDeck Spawnen
                cardDeck.GetComponent<CardDeckScript>().ShuffleDeck();
                cardDeck.GetComponent<CardDeckScript>().SpawnCardDeck();
                gameState = GameState.Enemy;
                break;

            case GameState.Enemy:
                //Gesamter Enemy Turn
                if (enemyField.GetComponent<Transform>().transform.childCount < 3)
                {
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                    //Debug.Log("Spawned Card");
                }
                gameState = GameState.PlayerCardDraw;
                break;

            case GameState.PlayerCardDraw:
                //Spieler Karten auffüllen
                if (hand.GetComponent<Transform>().transform.childCount < 5)
                {
                    for (int i = hand.GetComponent<Transform>().transform.childCount; i < 5; i++)
                    {
                        cardDeck.GetComponent<CardDeckScript>().MoveCardToHand();
                    }
                }
                gameState = GameState.PlayerIdle;
                break;

            case GameState.PlayerIdle:
                break;

        }
    }

    public void CardClicked(GameObject clickedOn)
    {
        Debug.Log("Backstäääähhhh");
        if (clicked02 == null && clicked01 == null && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Human)
        {
            clicked01 = clickedOn;
        }
        else if (clicked01 != null && clickedOn != clicked01 && clickedOn.GetComponent<OneCardManager>().cardAsset.cardType == CardType.Enemy)
        {
            clicked02 = clickedOn;
        } else
        {
            clicked01 = null;
            clicked02 = null;
        }

        if (clicked01 != null && clicked02 != null)
        {
            BasicAttack();
            clicked01 = null;
            clicked02 = null;
        }
    }

    private void BasicAttack()
    {
        clicked01.GetComponent<OneCardManager>().Health = clicked01.GetComponent<OneCardManager>().Health - clicked02.GetComponent<OneCardManager>().attack;
        clicked02.GetComponent<OneCardManager>().Health = clicked02.GetComponent<OneCardManager>().Health - clicked01.GetComponent<OneCardManager>().attack;
    }
}