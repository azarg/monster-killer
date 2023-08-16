using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttack : AttackBase
{
    private EnemyType enemyType;
    private List<Enemy> result;
    private float damage = 10f;

    public override float GetDamage() {
        return damage;
    }


    public override List<Enemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {

        result = new List<Enemy>();
        this.enemyType = enemy.enemyType;
        AddEnemy(enemy);
        return result;
    }

    private void AddEnemy(Enemy enemy) {

        var grid = enemyGrid.enemies;

        if (result.Contains(enemy)) {
            return;
        }
        
        result.Add(enemy);

        int row, col;

        row = enemy.row - 1; col = enemy.col - 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType) 
                AddEnemy(grid[row, col]);

        row = enemy.row - 1; col = enemy.col;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);

        row = enemy.row - 1; col = enemy.col + 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);

        row = enemy.row; col = enemy.col - 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);

        row = enemy.row; col = enemy.col + 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);

        row = enemy.row + 1; col = enemy.col - 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);
        
        row = enemy.row + 1; col = enemy.col;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);
        
        row = enemy.row + 1; col = enemy.col + 1;
        if (EnemyExistsAt(row, col) && grid[row, col].enemyType == enemyType)
            AddEnemy(grid[row, col]);
        return;
    }
}
