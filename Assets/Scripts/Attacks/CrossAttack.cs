using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAttack : AttackBase
{
    private float damage = 10f;

    public override float GetDamage() {
        return damage;
    }

    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {

        var grid = enemyGrid.enemies;
        int col = enemy.col;
        int row = enemy.row;

        var enemies = new List<Enemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col))
                enemies.Add(grid[i, col]);
        }

        for (int i = 0; i < grid.GetLength(1); i++) {
            if (EnemyExistsAt(row, i))
                enemies.Add(grid[row, i]);
        }

        return enemies;
    }
}
