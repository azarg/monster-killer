using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack : AttackBase
{
    private float baseDamage = 10f;
    
    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int col = cell.col;

        var enemies = new List<AttackedEnemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col)) {
                var attackedEnemy = new AttackedEnemy {
                    enemy = grid[i, col].enemy,
                    damage = this.baseDamage
                };
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}