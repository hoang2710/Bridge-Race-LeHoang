using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.LoadLevel:
                OnGameStateLoadLevel();
                break;
            case GameState.Playing:
                OnGameStatePlaying();
                break;
            case GameState.EndLevel:
                OnGameStateEndLevel();
                break;
            case GameState.ResultPhase:
                OnGameStateResultPhase();
                break;
            case GameState.MainMenu:
                OnGameStateMainMenu();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }

    public void OnGameStateLoadLevel()
    {
        Debug.Log("GameStateLoadLevel");
    }
    public void OnGameStatePlaying()
    {
        Debug.Log("GameStatePlaying");
    }
    public void OnGameStateEndLevel()
    {
        Debug.Log("GameStateEndLevel");
    }
    public void OnGameStateResultPhase()
    {
        Debug.Log("GameStateResultPhase");
    }
    public void OnGameStateMainMenu()
    {
        Debug.Log("GameStateMainMenu");
    }


    public enum GameState
    {
        LoadLevel,
        Playing,
        EndLevel,
        ResultPhase,
        MainMenu
    }
}
