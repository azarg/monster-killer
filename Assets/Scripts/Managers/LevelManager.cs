using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;

    [SerializeField] GameObject monsterGrid;
    [SerializeField] GameObject gridCellPrefab;
    [SerializeField] RectTransform monsterPanel;
    [SerializeField] float gridPadding;

    [SerializeField] BattleManager battleManager;

    public void StartLevel(int index) {
        // clear the grid
        foreach(Transform child in monsterGrid.transform) {
            Destroy(child.gameObject);
        }

        // add cells and enemies
        var level = levels[index];
        var grid = monsterGrid.GetComponent<GridLayoutGroup>();
        for (int i = 0; i < level.rows * level.columns; i++) {
            var cell = Instantiate(gridCellPrefab, grid.transform);
            var enemy = level.enemies[Random.Range(0, level.enemies.Length)];
            var enemyObj = Instantiate(enemy.prefab, cell.transform);
        }
        
        // set fixed row count
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = level.rows;

        // resize the monster panel
        Vector2 cellSize = grid.cellSize + grid.spacing;
        float gridWidth = cellSize.x * level.columns + gridPadding;
        float gridHeight = cellSize.y * level.rows + gridPadding;
        monsterPanel.sizeDelta = new Vector2(gridWidth, gridHeight);

        // reset the battle manager
        battleManager.StartBattle();
    }
}
