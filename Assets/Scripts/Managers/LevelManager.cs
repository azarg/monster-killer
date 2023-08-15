using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public EnemyController[,] enemies;
    public Level currentLevel;
    public static LevelManager Instance;
    [SerializeField] GameObject monsterGrid;
    [SerializeField] GameObject gridCellPrefab;
    [SerializeField] RectTransform monsterPanel;
    [SerializeField] float gridPadding;

    [SerializeField] BattleManager battleManager;

    private void Awake() {
        Instance = this;
    }
    public void StartLevel(int index) {
        // clear the grid
        foreach(Transform child in monsterGrid.transform) {
            Destroy(child.gameObject);
        }
        
        // add cells and enemies
        var level = levels[index];
        currentLevel = level;
        enemies = new EnemyController[level.rows, level.columns];
        var grid = monsterGrid.GetComponent<GridLayoutGroup>();
        for (int i = 0; i < level.rows; i++) {
            for (int j = 0; j < level.columns; j++) {
                var cell = Instantiate(gridCellPrefab, grid.transform);
                var enemy = level.enemies[Random.Range(0, level.enemies.Length)];
                var enemyObj = Instantiate(enemy.prefab, cell.transform);
                var enemyController = enemyObj.GetComponent<EnemyController>();
                enemyController.row = i;
                enemyController.col = j;
                enemies[i, j] = enemyController;
            }
        }

        // set fixed row count
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = level.rows;

        // resize the monster panel
        monsterPanel.gameObject.SetActive(true);
        Vector2 cellSize = grid.cellSize + grid.spacing;
        float gridWidth = cellSize.x * level.columns + gridPadding;
        float gridHeight = cellSize.y * level.rows + gridPadding;
        monsterPanel.sizeDelta = new Vector2(gridWidth, gridHeight);

        // reset the battle manager
        battleManager.StartBattle();
    }
}
