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

    public override List<Enemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;
        int col = cell.col;
        var enemies = new List<Enemy>();
        if (EnemyExistsAt(row, col)) {
            enemies.Add(grid[row, col].enemy);
        }
        return enemies;
    }
}