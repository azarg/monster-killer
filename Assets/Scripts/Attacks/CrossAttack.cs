using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAttack : AttackBase
{
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<Enemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int col = cell.col;
        int row = cell.row;

        var enemies = new List<Enemy>();

        for (int i = 0; i < grid.GetLength(0); i++) {
            if (EnemyExistsAt(i, col))
                enemies.Add(grid[i, col].enemy);
        }

        for (int i = 0; i < grid.GetLength(1); i++) {
            if (EnemyExistsAt(row, i))
                enemies.Add(grid[row, i].enemy);
        }

        return enemies;
    }
}
