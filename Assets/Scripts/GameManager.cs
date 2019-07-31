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

        if (gameState == GameState.GameStart)
        {
            //KartenDeck Spawnen
            cardDeck.GetComponent<CardDeckScript>().ShuffleDeck();
            cardDeck.GetComponent<CardDeckScript>().SpawnCardDeck();
            gameState = GameState.Enemy;
        }
        else if (gameState == GameState.Enemy)
        {
            //Gesamter Enemy Turn
            if (enemyField.GetComponent<Transform>().transform.childCount < 3)
            {
                enemyField.GetComponent<EnemyFieldScript>().SpawnNewEnemy();
                Debug.Log("Spawned Card");
            }
            gameState = GameState.PlayerCardDraw;
        }
        else if (gameState == GameState.PlayerCardDraw)
        {
            //Spieler Karten auffüllen
            if (hand.GetComponent<Transform>().transform.childCount < 5)
            {
                for (int i = hand.GetComponent<Transform>().transform.childCount; i < 5; i++)
                {
                    cardDeck.GetComponent<CardDeckScript>().MoveCardToHand();
                }
            }
            gameState = GameState.PlayerIdle;
        }
        else if (gameState == GameState.PlayerIdle)
        {
            //Bei start reset aller Highlights
            //Warten auf Player Input
        }
        else if (gameState == GameState.PlayerCardInHand)
        {
            //Highlighting + Klickable
            //Abhängig vom typ der Karte
        }
        else if (gameState == GameState.PlayerAttack)
        {
            //Highlighting + Klickable
            //Kommunikation der Werte und ausführen der aktion
        }
        else if (gameState == GameState.PlayerAbility)
        {
            //Highlighting + Klickable
            //Ausführen des Spells
        }
        
    }

    void BaseAttack()
    {

    }
}
