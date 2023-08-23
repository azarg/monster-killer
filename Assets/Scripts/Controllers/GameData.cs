using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public event Action OnPlayerHealthChanged;

    public Enemy[,] grid { get; private set; }
    public List<Enemy> enemies { get; private set; }

    public int enemyCount { get; private set; }

    public float playerHealth;
    public float maxPlayerHealth;
    public float estimatedPlayerHealthAfterFight;

    private bool isInitialized = false;

    private void OnEnable() {
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged = null;
        grid = null;
        enemies = null;
        enemyCount = 0;
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
        enemyCount = 0;
        isInitialized = true;
    }

    public void AddEnemy(Enemy enemy) {
        if (!isInitialized) throw new Exception("Grid not initialized");
        grid[enemy.row, enemy.col] = enemy;
        enemies.Add(enemy);
        enemyCount++;
    }

    internal void RemoveEnemy(Enemy enemy) {
        if (!isInitialized) throw new Exception("Grid not initialized");
        grid[enemy.row, enemy.col] = null;
        enemies.Remove(enemy);
        enemyCount--;
    }
}
