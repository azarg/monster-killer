using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack : AttackBase
{
    private float baseDamage = 10f;
    
    public override float GetDamage() {
        return baseDamage;
    }

    public override List<Enemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int col = cell.col;

        var enemies = new List<Enemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col)) {
                var attackedEnemy = grid[i, col].enemy;
                enemies.Add(attackedEnemy);
            }
        }

        return enemies;
    }
}