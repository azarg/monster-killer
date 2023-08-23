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

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = enemy.row;

        var enemies = new List<AttackedEnemy>();

        for (int i = enemy.col; i < Mathf.Min(grid.GetLength(1), enemy.col + attackDepth); i++) {
            if (EnemyExistsAt(row, i)) {
                var attackedEnemy = new AttackedEnemy {
                    enemy = grid[row, i],
                    damage = this.baseDamage / i
                };
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}