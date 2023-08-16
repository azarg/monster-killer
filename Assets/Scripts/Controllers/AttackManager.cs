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

        List<AttackedEnemy> attackedEnemies = attack.GetAttackedEnemies(targetEnemy, mousePosition);
        foreach (AttackedEnemy attackedEnemy in attackedEnemies) {
            attackedEnemy.enemy.ApplyDamage(attack.GetDamage());
        }

        enemyGrid.RemoveAttackHighlight();
    }
}