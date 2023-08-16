using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public virtual List<Enemy> GetAttackedEnemies(Enemy selectedEnemy, Enemy[,] grid, Vector3 mousePosition) {
        throw new System.NotImplementedException();
    }
    public virtual float GetDamage() {
        throw new System.NotImplementedException();
    }

    protected bool EnemyExistsAt(int row, int col, Enemy[,] grid) {
        if (row < grid.GetLength(0) && row > -1 &&
            col < grid.GetLength(1) && col > -1 &&
            grid[row, col] != null) {
            return true;
        }

        return false;
    }
}