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

    public void HighlightAttackedEnemies(Cell target_cell, Vector3 mousePosition) {
        if (currentAttack == null) return;

        RemoveAttackHighlight();

        List<Enemy> attackedEnemies = currentAttack.GetAttackedEnemies(target_cell, mousePosition);
        foreach (var attackedEnemy in attackedEnemies) {
            attackedEnemy.Highlight(currentAttack);
        }
    }

    public void RemoveAttackHighlight() {
        foreach (var cell in gameManager.grid) {
            if (cell.enemy != null) {
                cell.enemy.RemoveHighlight();
            }
        }
    }

    public void ApplyCurrentAttack(Cell target_cell, Vector3 mousePosition) {
        if (currentAttack == null) {
            return;
        }

        List<Enemy> attackedEnemies = currentAttack.GetAttackedEnemies(target_cell, mousePosition);
        foreach (Enemy attackedEnemy in attackedEnemies) {
            attackedEnemy.Hurt(currentAttack.GetDamage());
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
        foreach (var cell in gameManager.grid) {
            if (cell.enemy != null && cell.enemy != attackedEnemy) {
                if (cell.enemy.IsAttacking()) {
                    cell.enemy.Attack();
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
        foreach (var cell in gameManager.grid) {
            if (cell.enemy != null && cell.enemy != attackedEnemy) {
                if (cell.enemy.IsAttacking()) {
                    enemyDPS += cell.enemy.GetDamage();
                }
            }
        }

        // calculate estimated player health after fight
        var turns_for_enemy_to_die = Mathf.Ceil(attackedEnemy.currentHealth / player.EstimatedDamage());
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