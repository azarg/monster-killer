using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AttackBase
{
    private float baseDamage = 10f;

    public override float GetDamage() {
        return baseDamage;
    }

    public override List<AttackedEnemy> GetAttackedEnemies(Enemy enemy, Vector3 mousePosition) {
        var grid = gameData.enemies;
        int row = enemy.row;
        int col = enemy.col;

        var colDirection = (int) Mathf.Sign(mousePosition.x - enemy.transform.position.x);
        var rowDirection = (int) Mathf.Sign(enemy.transform.position.y - mousePosition.y);

        var enemies = new List<AttackedEnemy>();

        if (EnemyExistsAt(row, col))
            enemies.Add(new AttackedEnemy() {
                enemy = grid[row, col],
                damage = baseDamage
            });

        if (EnemyExistsAt(row + rowDirection, col))
            enemies.Add(new AttackedEnemy(){
                enemy = grid[row + rowDirection, col],
                damage = baseDamage 
            });

        if (EnemyExistsAt(row, col + colDirection))
            enemies.Add(new AttackedEnemy() {
                enemy = grid[row, col + colDirection],
                damage = baseDamage
            });

        if (EnemyExistsAt(row + rowDirection, col + colDirection))
            enemies.Add(new AttackedEnemy() { 
                enemy = grid[row + rowDirection, col + colDirection], 
                damage = baseDamage 
            });

        return enemies;
    }
}