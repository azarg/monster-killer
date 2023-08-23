using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Default, InBattle }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnPlayerHealthChanged;
    public LevelManager levelManager;
    public GameState gameState;

    public float playerHealth;
    public float maxPlayerHealth = 100f;
    public float estimatedPlayerHealthAfterFight;

    public Enemy[,] grid { get; private set; }
    public List<Enemy> enemies { get; private set; }

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

    public void ChangeGameState(GameState state) {
        gameState = state;
        if (gameState == GameState.Default) {
            attackPanel.SetParent(attackPanel_default_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(false);
        } 
        if (gameState == GameState.InBattle) {
            attackPanel.SetParent(attackPanel_battle_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(true);
        }
    }

    public void SetPlayerHealth(float amount) {
        playerHealth = amount;
        OnPlayerHealthChanged?.Invoke();
    }

    public void ChangePlayerHealth(float amount) {
        playerHealth += amount;
        OnPlayerHealthChanged?.Invoke();
    }

    public float GetDPS() {
        return 1f;
    }

    public void InitializeGrid(int rows, int cols) {
        grid = new Enemy[rows, cols];
        enemies = new List<Enemy>();
        isInitialized = true;
    }

    public void AddEnemy(Enemy enemy) {
        if (!isInitialized) throw new Exception("Grid not initialized");
        grid[enemy.row, enemy.col] = enemy;
        enemies.Add(enemy);
    }

    internal void RemoveEnemy(Enemy enemy) {
        if (!isInitialized) throw new Exception("Grid not initialized");
        grid[enemy.row, enemy.col] = null;
        enemies.Remove(enemy);
    }
}
