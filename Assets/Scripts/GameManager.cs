using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool GetPowers()
    {
        return hasPowers;
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void OnStartCutScene()
    {
        gameState = GameState.GAMEPLAY;
        hasPowers = true;
        string[] speeches = new string[] {"What happened? Am I dead?", "I feel a strange tingling on my hands...", "Press F and see what happens"};
        dialogueManager.StartDialogue(speeches);

    }

    public void OnToxicDeath()
    {
        gameState = GameState.CUTSCENE;
    }
}