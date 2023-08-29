using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] Level[] sequencedLevels;

    [SerializeField] RectTransform monsterPanel;


    [Header("Monster grid")]
    [SerializeField] GameObject monsterGrid;
    [SerializeField] float monsterGridPadding = 20f;
    [SerializeField] GameObject gridCellPrefab;

    private Level currentLevel;

    private void Start() {
        gameManager = GameManager.Instance;
        UpdateLevelDisplay();
    }

    private void OnEnable() {
        Enemy.OnEnemyDied += Enemy_OnEnemyDied;
    }

    private void OnDisable() {
        Enemy.OnEnemyDied -= Enemy_OnEnemyDied;
    }
    private void Enemy_OnEnemyDied() {
        if (gameManager.enemies.Count <= 0) {
            currentLevel.isCompleted = true;
            UpdateLevelDisplay();
            GameManager.Instance.ChangeGameState(GameState.Default);
        }
    }

    private void UpdateLevelDisplay() {
        bool foundPlayableLevel = false;

        for (int i = 0; i <  sequencedLevels.Length; i++) {
            var level = sequencedLevels[i];
            if (level.isCompleted) {
                level.MarkAsCompleted();
            }
            if (level.isCompleted == false) {
                if (foundPlayableLevel) {
                    level.MarkAsHidden();
                }
                else {
                    level.MarkAsCurrent();
                    foundPlayableLevel = true;
                }
            }

        }
    }

    public void HandleLevelSelected(Level level) {
        //completed levels cannot be played again
        if (level.isCompleted) return;

        StartLevel(level);
    }

    public void StartLevel(Level level) {
        
        currentLevel = level;

        GameManager.Instance.ChangeGameState(GameState.InBattle);
        
        gameManager.player.ResetHealth();

        // clear the grid
        foreach (Transform child in monsterGrid.transform) {
            Destroy(child.gameObject);
        }

        // add cells and enemies
        gameManager.InitializeGrid(level.rows, level.columns);

        var grid = monsterGrid.GetComponent<GridLayoutGroup>();
        for (int i = 0; i < level.rows; i++) {
            for (int j = 0; j < level.columns; j++) {
                var cell = Instantiate(gridCellPrefab, grid.transform);
                var enemyType = level.enemyTypes[Random.Range(0, level.enemyTypes.Length)];
                var enemyGameObject = Instantiate(enemyType.prefab, cell.transform);
                var enemy = enemyGameObject.GetComponent<Enemy>();
                enemy.row = i;
                enemy.col = j;
                gameManager.AddEnemy(enemy);
            }
        }

        // set fixed row count
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = level.rows;

        // resize the monster panel
        monsterPanel.gameObject.SetActive(true);
        Vector2 cellSize = grid.cellSize + grid.spacing;
        float gridWidth = cellSize.x * level.columns + monsterGridPadding;
        float gridHeight = cellSize.y * level.rows + monsterGridPadding;
        monsterPanel.sizeDelta = new Vector2(gridWidth, gridHeight);

    }
}
