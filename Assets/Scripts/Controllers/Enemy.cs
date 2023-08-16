using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] AttackManager attackManager;
    [SerializeField] EnemyGrid enemyGrid;
    [SerializeField] Image highlightImage;
    public EnemyType enemyType;

    public int row;
    public int col;

    private Image enemyImage;
    private float currentHealth;

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
        Debug.Log($"damaged enemy at {row},{col} for {damage}");
        currentHealth -= damage;
        if( currentHealth <= 0 ) {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        attackManager.ApplyCurrentAttack(this, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData) {
        enemyGrid.RemoveAttackHighlight();
    }

    public void OnPointerMove(PointerEventData eventData) {
        enemyGrid.HighlightAttackedEnemies(this, Input.mousePosition);
    }


}
