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

        List<Enemy> attackedEnemies = attack.GetAttackedEnemies(targetEnemy, enemies, mousePosition);
        foreach (var enemy in enemies) {
            if (attackedEnemies.Contains(enemy)) {
                enemy.Highlight(attack);
            }
            else {
                enemy.RemoveHighlight();
            }
        }
    }

    public void RemoveAttackHighlight() {
        foreach (var enemy in enemies) {
            if (enemy != null) {
                enemy.RemoveHighlight();
            }
        }
    }

    public void ApplyCurrentAttack(Enemy targetEnemy, Vector3 mousePosition) {
        var attack = gameData.currentAttack;
        if (attack == null) return;

        List<Enemy> attackedEnemies = attack.GetAttackedEnemies(targetEnemy, enemies, mousePosition);
        foreach (Enemy enemy in attackedEnemies) {
            enemy.ApplyDamage(attack.GetDamage());
        }
    }
}
