using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : AttackBase
{
    private int attackDepth = 4;

    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        int row = enemy.row;

        var enemies = new List<EnemyController>();

        for (int i = 0; i < Mathf.Min(grid.GetLength(1), attackDepth); i++) {
            if (EnemyExistsAt(row, i, grid))
                enemies.Add(grid[row, i]);
        }

        return enemies;
    }
}