using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {GAMESTART, GAMEPLAY, CUTSCENE, DEATH};
    private GameState gameState;

    [SerializeField] DialogueManager dialogueManager;
    public GameObject player;

    bool hasPowers = false;

    void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //State
            gameState = GameState.GAMESTART;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        handleGameState();
    }

    public GameState GetState()
    {
        return gameState;
    }

    void handleGameState()
    {
        switch(gameState)
        {
            case GameState.GAMESTART:
                OnGameStart();
                break;
            case GameState.CUTSCENE:
                OnStartCutScene();
                break;
            case GameState.DEATH:
                OnDeath();
                break;
        }
    }

    public void OnGameStart()
    {
        gameState = GameState.GAMEPLAY;
        Debug.Log("OnGameStart");
        string[] speeches = new string[] { "Hey there! My name is Thomas!", "But we don't have time for small talk they are after me!", "Help me move with the WASD or Arrow keys! I Jump with Space!", "Help me protect myself with Ctrl!", "And please try not to get me killed! Thank you!"};
        dialogueManager.StartDialogue(speeches);
        
    }

    public void OnDeath()
    {

    }

    public void OnStartCutScene()
    {

    }
}