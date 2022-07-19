using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static event Action<GameState> OnGameStateChanged;
    #region Singleton
    public static GameManager Instance { get; private set; }
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
    #endregion

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
