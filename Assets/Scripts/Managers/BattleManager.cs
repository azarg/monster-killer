using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;



public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameData gameData;

    public AttackBase currentAttack;

    private void Awake() {
        Instance = this;
    }

    public void StartBattle() {
        gameData.ResetPlayerHealth(10,10);
    }

    public void SelectMelee() {
        currentAttack = new MeleeAttack();
    }
    public void SelectRanged() {
        currentAttack = new MeleeAttack();
    }

    public void SelectFrost() {
        currentAttack = new FrostAttack();
    }

    public void UsePotion1() {

    }

    public void UsePotion2() {

    }

    public void SelectFireball() {
        currentAttack = new FireballAttack();
    }

    public void HighlightAttackedEnemies(EnemyController enemyCtrl, Vector3 mousePosition) {
        List<EnemyController> attackedEnemies = GetAttackedEnemies(enemyCtrl, mousePosition);
        foreach (var enemy in levelManager.enemies) {
            if (attackedEnemies.Contains(enemy)) {
                enemy.Highlight(currentAttack);
            }
            else {
                enemy.RemoveHighlight();
            }
        }
    }

    public void RemoveAttackHighlight() {
        foreach (var enemy in levelManager.enemies) {
            if (enemy != null) {
                enemy.RemoveHighlight();
            }
        }
    }

    public void ApplyCurrentAttack(EnemyController enemyCtrl, Vector3 mousePosition) {
        Debug.Log("applied current attack");
        List<EnemyController> attackedEnemies = GetAttackedEnemies(enemyCtrl, mousePosition);
        foreach(EnemyController enemy in attackedEnemies) {
            if (enemy != null) {
                Debug.Log($"row={enemy.row}, col={enemy.col}");
            }
        }
    }

    public List<EnemyController> GetAttackedEnemies(EnemyController enemyCtrl, Vector3 mousePosition) {
        List<EnemyController> attackedEnemies = currentAttack.GetAttackedEnemies(enemyCtrl, levelManager.enemies, mousePosition);
        return attackedEnemies;
    }
}
