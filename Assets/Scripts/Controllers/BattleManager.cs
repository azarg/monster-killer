using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour 
{
    public static BattleManager Instance;

    public GameData gameData;

    private AttackBase currentAttack;

    private void Awake() {
        Instance = this;
    }

    public bool IsAttackSelected() {
        return currentAttack != null;
    }

    public void SelectAttack(AttackBase attack) {
        currentAttack = attack;
    }

    public void DeselectAttack() {
        currentAttack = null;
        RemoveAttackHighlight();
    }

    public void HighlightAttackedEnemies(Enemy targetEnemy, Vector3 mousePosition) {
        if (currentAttack == null) return;

        RemoveAttackHighlight();

        List<AttackedEnemy> attackedEnemies = currentAttack.GetAttackedEnemies(targetEnemy, mousePosition);
        foreach (var attackedEnemy in attackedEnemies) {
            attackedEnemy.enemy.Highlight(currentAttack);
        }
    }

    public void RemoveAttackHighlight() {
        if (gameData.enemies == null) return;
        foreach (var enemy in gameData.enemies) {
            if (enemy != null) {
                enemy.RemoveHighlight();
            }
        }
    }

    public void ApplyCurrentAttack(Enemy enemy, Vector3 mousePosition) {
        if (currentAttack == null) {
            enemy.Fight();
            return;
        };

        List<AttackedEnemy> attackedEnemies = currentAttack.GetAttackedEnemies(enemy, mousePosition);
        foreach (AttackedEnemy attackedEnemy in attackedEnemies) {
            attackedEnemy.enemy.ApplyDamage(currentAttack.GetDamage());
        }

        RemoveAttackHighlight();
    }
}