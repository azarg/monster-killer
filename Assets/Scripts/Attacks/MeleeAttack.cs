using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttack : AttackBase
{
    private float damage = 10f;

    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Enemy[,] grid, Vector3 mousePosition) {
        int row = enemy.row;
        int col = enemy.col;
        var enemies = new List<Enemy>();
        if (EnemyExistsAt(row, col, grid)) {
            enemies.Add(grid[row, col]);
        }
        return enemies;
    }

    public override float GetDamage() {
        return damage;
    }
}