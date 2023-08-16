using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedEnemy
{
    public Enemy enemy;
    public float damage;
}

public class AttackBase : MonoBehaviour
{
    protected EnemyGrid enemyGrid;

    private void Start() {
        enemyGrid = GameManager.Instance.EnemyGrid;
    }

    public virtual List<AttackedEnemy> GetAttackedEnemies(Enemy selectedEnemy, Vector3 mousePosition) {
        return new List<AttackedEnemy>();
    }

    public virtual float GetDamage() {
        return 0;
    }

    protected bool EnemyExistsAt(int row, int col) {
        var grid = enemyGrid.enemies;
        if (row < grid.GetLength(0) && row > -1 &&
            col < grid.GetLength(1) && col > -1 &&
            grid[row, col] != null) {
            return true;
        }

        return false;
    }
}