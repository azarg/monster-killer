using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] Image highlightImage;
    [SerializeField] Image damageIndicator;
    
    public EnemyType enemyType;

    public int row;
    public int col;

    private Image enemyImage;
    private float currentHealth;
    private float defaultAttackWaitTime = 0.2f;
    private float defaultDamageTaken = 1f;
    private float defaultDamageCaused = 1f;

    private void Awake() {
        highlightImage.gameObject.SetActive(false);
        enemyImage = gameObject.GetComponent<Image>();
        enemyImage.sprite = enemyType.sprite;
        currentHealth = enemyType.startingHealth;
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

    public void ApplyDamage(float damage) {
        currentHealth -= damage;
        if( currentHealth <= 0 ) {
            Destroy(gameObject);
            return;
        }
        damageIndicator.fillAmount = (enemyType.startingHealth - currentHealth)/enemyType.startingHealth;
    }

    public void Fight() {
        InvokeRepeating(nameof(PerformSingleDefaultAttack), 0, defaultAttackWaitTime);
    }

    private void PerformSingleDefaultAttack() {
        ApplyDamage(defaultDamageTaken);
    }

    public float GetPotentialDamage() {
        float potentialDamage = enemyType.maxDamage * (currentHealth / enemyType.startingHealth);
        return potentialDamage;
    }
}
