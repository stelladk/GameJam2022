using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {GAMESTART, GAMEPLAY, CUTSCENE, DEATH};
    private GameState gameState;

    DialogueManager dialogueManager;
    public GameObject player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemySpawnRateMax = 15f;
    [SerializeField] float enemySpawnRateMin = 10f;
    [SerializeField] float enemySpawnRateDecrease = 1f;
    
    GameObject[] spawnPoints;

    ScoreBoard scoreBoard;
    int scorePoints = 0;

    bool hasPowers = false;
    private float enemySpawnRate;
    private bool canSpawnEnemies = true;

    void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
            //State
            gameState = GameState.GAMESTART;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("GameManager OnSceneLoaded"); 
        if(spawnPoints == null || spawnPoints.Length < 1){
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }
        enemySpawnRate = enemySpawnRateMax;

        player = GameObject.FindWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        scoreBoard.increaseScore(scorePoints);
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
            case GameState.GAMEPLAY:
                OnGamePlay();
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
    public void increaseScore(int points)
    {
        scorePoints += points;
        scoreBoard.increaseScore(points);
    }

    public void OnDeath()
    {
        StartCoroutine(ReloadScene());
    }

    public void OnStartCutScene()
    {
        gameState = GameState.GAMEPLAY;
        hasPowers = true;
        string[] speeches = new string[] {"What happened? Am I dead?", "I feel a strange tingling on my hands...", "Press F and see what happens"};
        dialogueManager.StartDialogue(speeches);

    }

    public void OnGamePlay()
    {
        if(canSpawnEnemies){
            StartCoroutine(SpawnEnemies());
        }
    }

    public void OnToxicDeath()
    {
        gameState = GameState.CUTSCENE;
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2);
        spawnPoints = null;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator SpawnEnemies(){
        canSpawnEnemies = false;
        // Spawn Enemies
        foreach (GameObject spawnPoint in spawnPoints){
            Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        yield return new WaitForSeconds(enemySpawnRate);
        if (enemySpawnRate > enemySpawnRateMin){
            enemySpawnRate -= enemySpawnRateDecrease;
        }
        canSpawnEnemies = true;
    }
}