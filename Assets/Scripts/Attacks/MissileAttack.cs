using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : AttackBase
{
    private int attackDepth = 4;
    private float damage = 10f;

    public override float GetDamage() {
        return damage;
    }

    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = enemyGrid.enemies;
        int row = enemy.row;

        var enemies = new List<Enemy>();

        for (int i = 0; i < Mathf.Min(grid.GetLength(1), attackDepth); i++) {
            if (EnemyExistsAt(row, i))
                enemies.Add(grid[row, i]);
        }

        return enemies;
    }
}