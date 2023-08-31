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

    public override List<AttackedEnemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;
        int col = cell.col;
        var enemies = new List<AttackedEnemy>();
        if (EnemyExistsAt(row, col)) {
            enemies.Add(new AttackedEnemy() { enemy = grid[row, col].enemy, damage = this.damage });
        }
        return enemies;
    }
}