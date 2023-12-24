using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action OnGameOver;
    public event Action OnGamePause;

    public enum GameState
    {
        Start,
        Playing,
        Pause,
        GameOver
    }

    private GameState gameState;

    private void Awake()
    {
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Start");
        gameState = GameState.Start;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                Debug.Log("gameState: Start");
                gameState = GameState.Playing;
                break;
            case GameState.Playing:
                Playing();
                break;
            case GameState.Pause:
                Pause();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    private void Playing()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Pause;
        }
    }

    private void Pause()
    {
        OnGamePause?.Invoke();
        Time.timeScale = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Playing;
            Time.timeScale = 1;
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnGameOver?.Invoke();
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}