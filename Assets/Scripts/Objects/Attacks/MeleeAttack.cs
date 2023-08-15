using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
}