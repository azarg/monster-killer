using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Enemy[,] grid, Vector3 mousePosition) {
        int row = enemy.row;
        int col = enemy.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - enemy.transform.position.x);
        var rowDirection = (int) Mathf.Sign(enemy.transform.position.y - mousePosition.y);

        var enemies = new List<Enemy>();

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
}