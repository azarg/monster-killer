using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerMoveHandler
{
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
        BattleManager.Instance.ApplyCurrentAttack(this, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData) {
        BattleManager.Instance.RemoveAttackHighlight();
    }

    public void OnPointerMove(PointerEventData eventData) {
        BattleManager.Instance.HighlightAttackedEnemies(this, Input.mousePosition);
    }
}
