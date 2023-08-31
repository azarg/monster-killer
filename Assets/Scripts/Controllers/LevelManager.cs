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
        gameManager.CurrentLevelCleared += CurrentLevelCleared;
        UpdateLevelDisplay();
    }

    private void CurrentLevelCleared() {
        currentLevel.isCompleted = true;
        UpdateLevelDisplay();
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

        // cannot play a level if player has no remaining health
        if (gameManager.player.remaining_health <= 0) return;

        StartLevel(level);
    }

    public void StartLevel(Level level) {
        
        currentLevel = level;

        GameManager.Instance.ChangeGameState(GameState.PlayingLevel);
        
        gameManager.player.ResetHealth();

        // clear the grid
        foreach (Transform child in monsterGrid.transform) {
            Destroy(child.gameObject);
        }

        // add cells and enemies
        gameManager.InitializeGrid(level.rows, level.columns);

        var grid = monsterGrid.GetComponent<GridLayoutGroup>();
        for (int row = 0; row < level.rows; row++) {
            for (int col = 0; col < level.columns; col++) {
                var cellObj = Instantiate(gridCellPrefab, grid.transform);
                var enemyType = level.enemyTypes[Random.Range(0, level.enemyTypes.Length)];
                var enemyGameObject = Instantiate(enemyType.prefab, cellObj.transform);
                var enemy = enemyGameObject.GetComponent<Enemy>();
                var cell = cellObj.GetComponent<Cell>();
                cell.enemy = enemy;
                cell.row = row;
                cell.col = col;
                gameManager.grid[row, col] = cell;
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
