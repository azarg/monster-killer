using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 
    Default, 
    PlayingLevel,
    Fighting
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action<GameState> StateChanged;
    public event Action CurrentLevelCleared;
    public GameState gameState;
    public Player player;

    public Cell[,] grid { get; private set; }

    [SerializeField] RectTransform monsterPanel;
    [SerializeField] RectTransform attackPanel;
    [SerializeField] RectTransform attackPanel_default_position;
    [SerializeField] RectTransform attackPanel_battle_position;


    private bool isInitialized = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start() {
        player.OnPlayerRemainingHealthChanged += OnPlayerRemainingHealthChanged;
    }

    private void OnPlayerRemainingHealthChanged() {
        if (player.remaining_health <= 0) {
            ChangeGameState(GameState.Default);
        }
    }

    public void ChangeGameState(GameState state) {
        gameState = state;
        if (gameState == GameState.Default) {
            attackPanel.SetParent(attackPanel_default_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(false);
        } 
        if (gameState == GameState.PlayingLevel) {
            attackPanel.SetParent(attackPanel_battle_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(true);
        }
        StateChanged?.Invoke(state);
    }

    public void InitializeGrid(int rows, int cols) {
        grid = new Cell[rows, cols];
        isInitialized = true;
    }

    public void RemoveEnemy(Enemy enemy) {
        if (!isInitialized) throw new Exception("Grid not initialized");
        
        foreach(var cell in grid) {
            if (cell.enemy == enemy) {
                cell.enemy = null;
                break;
            }
        }

        if (RemainingEnemyCount() == 0) {
            CurrentLevelCleared?.Invoke();
            ChangeGameState(GameState.Default);
        }
        else {
            MoveEnemies();
            if (player.remaining_health > 0) {
                // continue playing level if player has health
                ChangeGameState(GameState.PlayingLevel);
            }
        }
    }

    private int RemainingEnemyCount() {
        int count = 0;
        foreach(var cell in grid) {
            if (cell.enemy != null) {
                count++;
            }
        }
        return count;
    }
    private void MoveEnemies() {
        //start from top left corner, going right, then down
        //moving every enemy as far left as possible

        for (var r = 0; r < grid.GetLength(0); r++) {
            for (var c = 0; c < grid.GetLength(1); c++) {
                
                var from_cell = grid[r, c];
                if (from_cell.enemy == null) continue;

                for (var c1 = 0; c1 < c; c1++) {
                    
                    var to_cell = grid[r, c1];
                    if (to_cell.enemy != null) continue;
                    
                    MoveEnemy(from_cell, to_cell);
                    break;
                    
                }
            }
        }
    }

    private void MoveEnemy(Cell from,  Cell to) {
        to.enemy = from.enemy;
        from.enemy.transform.SetParent(to.transform, false);
        from.enemy = null;
    }
}
