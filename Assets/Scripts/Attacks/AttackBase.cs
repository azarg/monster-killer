using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    protected EnemyGrid enemyGrid;

    private void Start() {
        enemyGrid = GameManager.Instance.EnemyGrid;
    }

    public virtual List<Enemy> GetAttackedEnemies(Enemy selectedEnemy, Vector3 mousePosition) {
        throw new System.NotImplementedException();
    }

    public virtual float GetDamage() {
        throw new System.NotImplementedException();
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