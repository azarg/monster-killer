using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] Image highlightImage;
    [SerializeField] Image healthIndicator;
    
    public EnemyType enemyType;

    public int row;
    public int col;

    private Image enemyImage;
    public float currentHealth;

    private bool isFighting = false;
    private bool isTakingTurn = false;
    private float dpsBeforeFight;
    private float turnDelay = 0.1f;

    private void Awake() {
        highlightImage.gameObject.SetActive(false);
        enemyImage = gameObject.GetComponent<Image>();
        enemyImage.sprite = enemyType.sprite;
        currentHealth = enemyType.startingHealth;
    }

    private void Update() {
        if(isFighting) {
            if(isTakingTurn == false)
                StartCoroutine(nameof(TakeTurn));
        }
    }

    IEnumerator TakeTurn() {
        isTakingTurn = true;
        gameData.ChangePlayerHealth(-dpsBeforeFight);
        this.Hurt(gameData.GetDPS());

        if(gameData.playerHealth <= 0 || currentHealth <= 0) {
            isFighting = false;
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
            Destroy(gameObject);
            return;
        }
        healthIndicator.fillAmount = (enemyType.startingHealth - currentHealth)/enemyType.startingHealth;
    }

    public float GetDPS() {
        float dps = enemyType.maxDPS * (currentHealth / enemyType.startingHealth);
        return dps;
    }

    public bool IsAttacking() {
        // TODO: implement
        return false;
    }

    public void Fight() {
        isFighting = true;
        dpsBeforeFight = GetDPS();
    }
}

