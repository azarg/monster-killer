using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : AttackBase
{
    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        int row = enemy.row;

        var enemies = new List<EnemyController>();

        for (int i = 0; i < grid.GetLength(1); i++) {
            if (EnemyExistsAt(row, i, grid))
                enemies.Add(grid[row, i]);
        }

        return enemies;
    }
}