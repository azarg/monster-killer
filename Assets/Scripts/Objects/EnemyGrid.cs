using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyGrid : ScriptableObject
{
    [SerializeField] GameData gameData;
    public EnemyController[,] enemies;

    public void HighlightAttackedEnemies(EnemyController enemyCtrl, Vector3 mousePosition) {
        var attack = gameData.currentAttack;
        if (attack == null) return;

        List<EnemyController> attackedEnemies = attack.GetAttackedEnemies(enemyCtrl, enemies, mousePosition);
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

    public void ApplyCurrentAttack(EnemyController enemyCtrl, Vector3 mousePosition) {
        var attack = gameData.currentAttack;
        if (attack == null) return;

        List<EnemyController> attackedEnemies = attack.GetAttackedEnemies(enemyCtrl, enemies, mousePosition);
        foreach (EnemyController enemy in attackedEnemies) {
            if (enemy != null) {
                Debug.Log($"row={enemy.row}, col={enemy.col}");
            }
        }
    }
}
