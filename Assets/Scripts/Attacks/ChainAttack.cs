using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttack : AttackBase
{
    private EnemyType enemyType;
    private List<AttackedEnemy> attackedEnemies;
    private float baseDamage = 10f;
    private Enemy[,] grid;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {

        attackedEnemies = new List<AttackedEnemy>();
        this.enemyType = enemy.enemyType;
        this.grid = gameData.grid;
        AddEnemy(enemy);
        return attackedEnemies;
    }

    private void AddEnemy(Enemy enemy) {
        foreach(var attackedEnemy in attackedEnemies) {
            if (attackedEnemy.enemy == enemy) return;
        }
        
        attackedEnemies.Add(new AttackedEnemy() { enemy = enemy, damage = baseDamage });

        CheckAndAddEnemy(enemy.row - 1, enemy.col - 1, enemy);
        CheckAndAddEnemy(enemy.row - 1, enemy.col, enemy);
        CheckAndAddEnemy(enemy.row - 1, enemy.col + 1, enemy);
        CheckAndAddEnemy(enemy.row, enemy.col - 1, enemy);
        CheckAndAddEnemy(enemy.row, enemy.col + 1, enemy);
        CheckAndAddEnemy(enemy.row + 1, enemy.col - 1, enemy);
        CheckAndAddEnemy(enemy.row + 1, enemy.col, enemy);
        CheckAndAddEnemy(enemy.row + 1, enemy.col + 1, enemy);
        return;
    }

    private void CheckAndAddEnemy(int row, int col, Enemy enemy) {
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);
    }
}
