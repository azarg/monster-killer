using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] EnemyGrid enemyGrid;
    [SerializeField] Image enemyImage;
    [SerializeField] Image highlightImage;

    public int row;
    public int col;

    private void Awake() {
        highlightImage.gameObject.SetActive(false);
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

    public void OnPointerClick(PointerEventData eventData) {
        enemyGrid.ApplyCurrentAttack(this, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData) {
        enemyGrid.RemoveAttackHighlight();
    }

    public void OnPointerMove(PointerEventData eventData) {
        enemyGrid.HighlightAttackedEnemies(this, Input.mousePosition);
    }
}
