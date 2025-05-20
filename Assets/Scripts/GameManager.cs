using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Menu, Game, LevelComplete, GameOver
    }
    private GameState currentGameState;
    public static Action<GameState> OnGameStateChanged;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {

    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
        OnGameStateChanged?.Invoke(currentGameState);

        Debug.Log("Game State Changed: " + currentGameState);
    }

    public bool isGameState()
    {
        return currentGameState == GameState.Game;
    }
}