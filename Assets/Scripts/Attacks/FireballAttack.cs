using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    private float damage = 10f;

    public override float GetDamage() {
        return damage;
    }

    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = enemyGrid.enemies;
        int row = enemy.row;
        int col = enemy.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - enemy.transform.position.x);
        var rowDirection = (int) Mathf.Sign(enemy.transform.position.y - mousePosition.y);

        var enemies = new List<Enemy>();

        if (EnemyExistsAt(row, col))
            enemies.Add(grid[row, col]);

        if (EnemyExistsAt(row + rowDirection, col))
            enemies.Add(grid[row + rowDirection, col]);

        if (EnemyExistsAt(row, col + colDirection))
            enemies.Add(grid[row, col + colDirection]);

        if (EnemyExistsAt(row + rowDirection, col + colDirection))
            enemies.Add(grid[row + rowDirection, col + colDirection]);

        return enemies;
    }
}