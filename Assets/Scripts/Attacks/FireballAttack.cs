using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        int row = enemy.row;
        int col = enemy.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - enemy.transform.position.x);
        var rowDirection = (int) Mathf.Sign(enemy.transform.position.y - mousePosition.y);

        var enemies = new List<EnemyController>();

        if (EnemyExistsAt(row, col, grid))
            enemies.Add(grid[row, col]);

        if (EnemyExistsAt(row + rowDirection, col, grid))
            enemies.Add(grid[row + rowDirection, col]);

        if (EnemyExistsAt(row, col + colDirection, grid))
            enemies.Add(grid[row, col + colDirection]);

        if (EnemyExistsAt(row + rowDirection, col + colDirection, grid))
            enemies.Add(grid[row + rowDirection, col + colDirection]);

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