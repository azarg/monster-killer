using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<Enemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;
        int col = cell.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - cell.transform.position.x);
        var rowDirection = (int) Mathf.Sign(cell.transform.position.y - mousePosition.y);

        var enemies = new List<Enemy>();

        if (EnemyExistsAt(row, col))
            enemies.Add(grid[row, col].enemy);

        if (EnemyExistsAt(row + rowDirection, col))
            enemies.Add(grid[row + rowDirection, col].enemy);

        if (EnemyExistsAt(row, col + colDirection))
            enemies.Add(grid[row, col + colDirection].enemy);

        if (EnemyExistsAt(row + rowDirection, col + colDirection))
            enemies.Add(grid[row + rowDirection, col + colDirection].enemy);

        return enemies;
    }
}