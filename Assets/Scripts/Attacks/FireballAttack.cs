using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Cell cell, Vector3 mousePosition) {
        var grid = gameManager.grid;
        int row = cell.row;
        int col = cell.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - cell.transform.position.x);
        var rowDirection = (int) Mathf.Sign(cell.transform.position.y - mousePosition.y);

        var enemies = new List<AttackedEnemy>();

        if (EnemyExistsAt(row, col))
            enemies.Add(new AttackedEnemy() {
                enemy = grid[row, col].enemy,
                damage = baseDamage
            });

        if (EnemyExistsAt(row + rowDirection, col))
            enemies.Add(new AttackedEnemy(){
                enemy = grid[row + rowDirection, col].enemy,
                damage = baseDamage 
            });

        if (EnemyExistsAt(row, col + colDirection))
            enemies.Add(new AttackedEnemy() {
                enemy = grid[row, col + colDirection].enemy,
                damage = baseDamage
            });

        if (EnemyExistsAt(row + rowDirection, col + colDirection))
            enemies.Add(new AttackedEnemy() { 
                enemy = grid[row + rowDirection, col + colDirection].enemy, 
                damage = baseDamage 
            });

        return enemies;
    }
}