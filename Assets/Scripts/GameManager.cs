using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    starting,
    player,
    enemy
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.starting:
                break;
            case GameState.enemy:
                break;
            case GameState.player:
                break;
        }
    }
}
