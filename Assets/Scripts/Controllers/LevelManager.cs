using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public EnemyGrid enemyGrid;

    [SerializeField] RectTransform monsterPanel;

    [Header("Monster grid")]
    [SerializeField] GameObject monsterGrid;
    [SerializeField] float monsterGridPadding = 20f;
    [SerializeField] GameObject gridCellPrefab;

    private void Awake() {
        Instance = this;
    }

    public void StartLevel(Level level) {
        // clear the grid
        foreach (Transform child in monsterGrid.transform) {
            Destroy(child.gameObject);
        }
        
        // add cells and enemies
        enemyGrid.enemies = new Enemy[level.rows, level.columns];

        var grid = monsterGrid.GetComponent<GridLayoutGroup>();
        for (int i = 0; i < level.rows; i++) {
            for (int j = 0; j < level.columns; j++) {
                var cell = Instantiate(gridCellPrefab, grid.transform);
                var enemyType = level.enemies[Random.Range(0, level.enemies.Length)];
                var enemyGameObject = Instantiate(enemyType.prefab, cell.transform);
                var enemy = enemyGameObject.GetComponent<Enemy>();
                enemy.row = i;
                enemy.col = j;

                enemyGrid.enemies[i, j] = enemy;
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