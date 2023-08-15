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
}