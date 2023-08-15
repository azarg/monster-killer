using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack : AttackBase
{
    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        int col = enemy.col;

        var enemies = new List<EnemyController>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col, grid))
                enemies.Add(grid[i, col]);
        }

        return enemies;
    }

    private bool EnemyExistsAt(int row, int col, EnemyController[,] grid) {
        if (row < grid.GetLength(0) && row > -1 &&
            col < grid.GetLength(1) && col > -1 &&
            grid[row, col] != null) {
            return true;
        }
            
        return false;
    }
}