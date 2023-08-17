using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack : AttackBase
{
    private float baseDamage = 10f;
    
    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = gameData.enemies;
        int col = enemy.col;

        var enemies = new List<AttackedEnemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col)) {
                var attackedEnemy = new AttackedEnemy {
                    enemy = grid[i, col],
                    damage = this.baseDamage
                };
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}