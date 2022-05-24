using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {GAMESTART, GAMEPLAY, DEATH};
    private GameState gameState;

    [SerializeField] DialoguePanel dialoguePanel;

    void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //State
            gameState = GameState.GAMESTART;
            OnGameStart();
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public GameState GetState()
    {
        return gameState;
    }

    public void OnGameStart()
    {
        Debug.Log("OnGameStart");
        dialoguePanel.createDialogue("But we don't have time for small talk they are after me!", null, 0);
    }

    public void Death()
    {

    }
}