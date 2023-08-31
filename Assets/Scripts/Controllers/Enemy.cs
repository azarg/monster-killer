using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Image highlightImage;
    [SerializeField] Image healthIndicator;
    private GameManager gameManager;
    public EnemyType enemyType;

    private Image enemyImage;
    public float currentHealth;

    private bool isAttacking = false;
    private bool isBeingAttacked = false;
    private bool isTakingTurn = false;
    private float currentDamage;
    private float turnDelay = 0.1f;

    private void Awake() {
        gameManager = GameManager.Instance;
        highlightImage.gameObject.SetActive(false);
        enemyImage = gameObject.GetComponent<Image>();
        enemyImage.sprite = enemyType.sprite;
        currentHealth = enemyType.startingHealth;
    }

    private void Update() {
        if(isAttacking) {
            if(isTakingTurn == false)
                StartCoroutine(nameof(TakeTurn));
        }
    }

    IEnumerator TakeTurn() {
        isTakingTurn = true;
        gameManager.player.Hurt(currentDamage);

        if (isBeingAttacked) {
            this.Hurt(gameManager.player.EstimatedDamage());
        }

        if(gameManager.player.remaining_health <= 0 || currentHealth <= 0) {
            isAttacking = false;
            isBeingAttacked = false;
        }

        yield return new WaitForSeconds(turnDelay);
        isTakingTurn = false;
    }

    public void Highlight(AttackBase attack) {
        highlightImage.gameObject.SetActive(true);
        var color = enemyImage.color;
        color.a = 0.5f;
        enemyImage.color = color;
    }

    public void RemoveHighlight() {
        highlightImage.gameObject.SetActive(false);
        var color = enemyImage.color;
        color.a = 1f;
        enemyImage.color = color;
    }

    public void Hurt(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            gameManager.RemoveEnemy(this);
            Destroy(gameObject);
            return;
        }
        healthIndicator.fillAmount = (enemyType.startingHealth - currentHealth)/enemyType.startingHealth;
    }

    public float GetDamage() {
        float damage = enemyType.baseDamage * (currentHealth / enemyType.startingHealth);
        return damage;
    }

    public bool IsAttacking() {
        return isAttacking;
    }

    /// <summary>
    /// Enemy is attacking the player but is not being attacked by player
    /// Enemy does not take damage
    /// </summary>
    public void Attack() {
        isAttacking = true;
        isBeingAttacked = false;
        currentDamage = GetDamage();
    }
        
    /// <summary>
    /// Enemy is both attacking and being attacked by player.
    /// Both take damage.
    /// </summary>
    public void Fight() {
        isAttacking = true;
        isBeingAttacked = true;
        currentDamage = GetDamage();
    }
}

