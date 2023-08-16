using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyGrid : ScriptableObject
{
    [SerializeField] GameData gameData;
    public Enemy[,] enemies;

    public void HighlightAttackedEnemies(Enemy targetEnemy, Vector3 mousePosition) {
        var attack = gameData.currentAttack;
        if (attack == null) return;

        RemoveAttackHighlight();

        List<AttackedEnemy> attackedEnemies = attack.GetAttackedEnemies(targetEnemy, mousePosition);
        foreach(var attackedEnemy in attackedEnemies) {
            attackedEnemy.enemy.Highlight(attack);
        }
    }

    public void RemoveAttackHighlight() {
        foreach (var enemy in enemies) {
            if (enemy != null) {
                enemy.RemoveHighlight();
            }
        }
    }
}
