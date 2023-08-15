using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        int row = enemy.row;
        int col = enemy.col;
        var enemies = new List<EnemyController>();
        if (EnemyExistsAt(row, col, grid)) {
            enemies.Add(grid[row, col]);
        }
        return enemies;
    }
    private bool EnemyExistsAt(int row, int col, EnemyController[,] grid) {
        if (row < grid.GetLength(0) && col < grid.GetLength(0) && grid[row, col] != null)
            return true;
        return false;
    }
}