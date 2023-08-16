using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAttack : AttackBase
{
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {

        var grid = enemyGrid.enemies;
        int col = enemy.col;
        int row = enemy.row;

        var enemies = new List<AttackedEnemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col))
                enemies.Add(new AttackedEnemy() { 
                    enemy = grid[i, col], 
                    damage = baseDamage 
                });
        }

        for (int i = 0; i < grid.GetLength(1); i++) {
            if (EnemyExistsAt(row, i))
                enemies.Add(new AttackedEnemy() { 
                    enemy = grid[row, i], 
                    damage = baseDamage 
                });
        }

        return enemies;
    }
}
