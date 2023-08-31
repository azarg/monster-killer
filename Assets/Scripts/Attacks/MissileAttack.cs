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

    public override List<AttackedEnemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;

        var enemies = new List<AttackedEnemy>();

        for (int i = cell.col; i < Mathf.Min(grid.GetLength(1), cell.col + attackDepth); i++) {
            if (EnemyExistsAt(row, i)) {
                var attackedEnemy = new AttackedEnemy {
                    enemy = grid[row, i].enemy,
                    damage = this.baseDamage / i
                };
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}