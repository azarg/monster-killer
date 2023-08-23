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

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = gameData.grid;
        int row = enemy.row;
        int col = enemy.col;
        var enemies = new List<AttackedEnemy>();
        if (EnemyExistsAt(row, col)) {
            enemies.Add(new AttackedEnemy() { enemy = grid[row, col], damage = this.damage });
        }
        return enemies;
    }
}