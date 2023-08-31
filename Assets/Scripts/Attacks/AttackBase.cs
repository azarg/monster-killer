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
    protected GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
    }

    public virtual List<AttackedEnemy> GetAttackedEnemies(Cell target_cell, Vector3 mousePosition) {
        return new List<AttackedEnemy>();
    }

    public virtual float GetDamage() {
        return 0;
    }

    protected bool EnemyExistsAt(int row, int col) {
        if (row < gameManager.grid.GetLength(0) && row > -1 &&
            col < gameManager.grid.GetLength(1) && col > -1 &&
            gameManager.grid[row, col].enemy != null) {
            return true;
        }

        return false;
    }
}