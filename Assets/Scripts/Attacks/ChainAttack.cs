using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttack : AttackBase
{
    private EnemyType enemyType;
    private List<AttackedEnemy> attackedEnemies;
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Cell target_cell, Vector3 mousePosition) {
        if (target_cell.enemy == null) return base.GetAttackedEnemies(target_cell, mousePosition);

        attackedEnemies = new List<AttackedEnemy>();
        this.enemyType = target_cell.enemy.enemyType;
        AddEnemy(target_cell);
        return attackedEnemies;
    }

    private void AddEnemy(Cell cell) {
        foreach(var attackedEnemy in attackedEnemies) {
            if (attackedEnemy.enemy == cell.enemy) return;
        }
        
        attackedEnemies.Add(new AttackedEnemy() { enemy = cell.enemy, damage = baseDamage });

        CheckAndAddEnemy(cell.row - 1, cell.col - 1);
        CheckAndAddEnemy(cell.row - 1, cell.col);
        CheckAndAddEnemy(cell.row - 1, cell.col + 1);
        CheckAndAddEnemy(cell.row, cell.col - 1);
        CheckAndAddEnemy(cell.row, cell.col + 1);
        CheckAndAddEnemy(cell.row + 1, cell.col - 1);
        CheckAndAddEnemy(cell.row + 1, cell.col);
        CheckAndAddEnemy(cell.row + 1, cell.col + 1);
        return;
    }

    private void CheckAndAddEnemy(int row, int col) {
        var grid = gameManager.grid;
        if (EnemyExistsAt(row, col) && grid[row, col].enemy.enemyType == enemyType)
            AddEnemy(grid[row, col]);
    }
}
