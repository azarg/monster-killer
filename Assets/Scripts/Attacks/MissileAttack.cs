using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : AttackBase
{
    private int attackDepth = 4;
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<Enemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;

        var enemies = new List<Enemy>();

        for (int i = cell.col; i < Mathf.Min(grid.GetLength(1), cell.col + attackDepth); i++) {
            if (EnemyExistsAt(row, i)) {
                var attackedEnemy = grid[row, i].enemy;
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}