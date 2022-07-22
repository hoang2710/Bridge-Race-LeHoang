using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    public static event Action<GameState> OnGameStateChanged;
    private void Start()
    {
        StartCoroutine(DelayChangeGameState(GameState.Loading, 0.5f));
    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Loading:
                OnGameStateLoading();
                break;
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

    public void OnGameStateLoading()
    {
        Debug.Log("GameStateLoading");
        StartCoroutine(DelayChangeGameState(GameState.LoadLevel, 0.5f));
        LevelManager.Instance.LoadGame();
    }
    public void OnGameStateLoadLevel()
    {
        Debug.Log("GameStateLoadLevel");
        LevelManager.Instance.LoadLevel();
        StartCoroutine(DelayChangeGameState(GameState.Playing, 0.5f));
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
        Loading,
        LoadLevel,
        Playing,
        EndLevel,
        ResultPhase,
        MainMenu
    }

    IEnumerator DelayChangeGameState(GameState state, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeGameState(state);
    }
}
