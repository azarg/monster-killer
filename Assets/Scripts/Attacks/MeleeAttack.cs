using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttack : AttackBase
{
    private float damage = 10f;

    public override float GetDamage() {
        return damage;
    }

    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = enemyGrid.enemies;
        int row = enemy.row;
        int col = enemy.col;
        var enemies = new List<Enemy>();
        if (EnemyExistsAt(row, col)) {
            enemies.Add(grid[row, col]);
        }
        return enemies;
    }
}