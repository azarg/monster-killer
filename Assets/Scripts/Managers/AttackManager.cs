using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;



public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;

    [SerializeField] LevelManager levelManager;
    [SerializeField] GameData gameData;

    private AttackBase currentAttack;

    private void Awake() {
        Instance = this;
    }

    public void StartBattle() {
        gameData.ResetPlayerHealth(10, 10);
    }

    public void RemoveAttackHighlight() {
        foreach (var enemy in levelManager.enemies) {
            if (enemy != null) {
                enemy.RemoveHighlight();
            }
        }
    }

    public void SetCurrentAttack(AttackBase attack) {
        currentAttack = attack;
    }

    public void HighlightAttackedEnemies(EnemyController enemyCtrl, Vector3 mousePosition) {
        if (currentAttack == null) return;

        List<EnemyController> attackedEnemies = currentAttack.GetAttackedEnemies(enemyCtrl, levelManager.enemies, mousePosition);
        foreach (var enemy in levelManager.enemies) {
            if (attackedEnemies.Contains(enemy)) {
                enemy.Highlight(currentAttack);
            }
            else {
                enemy.RemoveHighlight();
            }
        }
    }

    public void ApplyCurrentAttack(EnemyController enemyCtrl, Vector3 mousePosition) {
        if (currentAttack == null) return;

        List<EnemyController> attackedEnemies = currentAttack.GetAttackedEnemies(enemyCtrl, levelManager.enemies, mousePosition);
        foreach (EnemyController enemy in attackedEnemies) {
            if (enemy != null) {
                Debug.Log($"row={enemy.row}, col={enemy.col}");
            }
        }
    }
}
