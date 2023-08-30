using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour 
{
    [SerializeField] Text estimatedPlayerHealthDisplay;
    public GameManager gameManager;
    public Player player;

    private AttackBase currentAttack;

    private void OnEnable() {
        gameManager = GameManager.Instance;
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
        foreach (var enemy in gameManager.enemies) {
            enemy.RemoveHighlight();
        }
    }

    public void ApplyCurrentAttack(Enemy enemy, Vector3 mousePosition) {
        if (currentAttack == null) {
            return;
        }

        List<AttackedEnemy> attackedEnemies = currentAttack.GetAttackedEnemies(enemy, mousePosition);
        foreach (AttackedEnemy attackedEnemy in attackedEnemies) {
            attackedEnemy.enemy.Hurt(currentAttack.GetDamage());
        }

        RemoveAttackHighlight();
    }

    public void HideEstimatedHealth() {
        estimatedPlayerHealthDisplay.gameObject.SetActive(false);
    }

    public void Fight(Enemy attackedEnemy) {
        // safety
        if (gameManager.player.remaining_health <= 0) return;

        // can only fight with one enemy at a time
        if (gameManager.gameState == GameState.Fighting) return;

        gameManager.ChangeGameState(GameState.Fighting);

        attackedEnemy.Fight();
        foreach (var enemy in gameManager.enemies) {
            if (enemy != attackedEnemy) {
                if (enemy.IsAttacking()) {
                    enemy.Attack();
                }
            }
        }
    }

    public void DisplayEstimatedPlayerHealthAfterFight(Enemy attackedEnemy) {
        if (gameManager.player.remaining_health <= 0) return;

        float enemyDPS = 0;

        // attackedEnemy is the one clicked on
        enemyDPS += attackedEnemy.GetDamage();

        // also need to add dps by other attacking enemies
        foreach (var enemy in gameManager.enemies) {
            if (enemy != attackedEnemy) {
                if (enemy.IsAttacking()) {
                    enemyDPS += enemy.GetDamage();
                }
            }
        }

        // calculate estimated player health after fight
        var turns_for_enemy_to_die = attackedEnemy.currentHealth / player.EstimatedDamage();
        var estimated_player_health_after_fight = player.remaining_health - turns_for_enemy_to_die * player.EstimatedHurt(enemyDPS);

        estimatedPlayerHealthDisplay.gameObject.SetActive(true);

        if (estimated_player_health_after_fight > 0) {
            estimatedPlayerHealthDisplay.text = $"{(int)estimated_player_health_after_fight}";
        }
        else {
            estimatedPlayerHealthDisplay.text = "---";
        }
    }
}