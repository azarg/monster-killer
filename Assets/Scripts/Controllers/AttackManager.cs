using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackManager : ScriptableObject
{
    public EnemyGrid enemyGrid;
    public GameData gameData;

    public void ApplyCurrentAttack(Enemy targetEnemy, Vector3 mousePosition) {
        var attack = gameData.currentAttack;
        if (attack == null) return;

        List<Enemy> attackedEnemies = attack.GetAttackedEnemies(targetEnemy, enemyGrid.enemies, mousePosition);
        foreach (Enemy enemy in attackedEnemies) {
            enemy.ApplyDamage(attack.GetDamage());
        }
    }
}