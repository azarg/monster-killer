using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public BattleManager battleManager;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    private bool isAttackHighlighted;

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            battleManager.DeselectAttack();
            return;
        }
        
        bool click = Input.GetMouseButtonDown(0);
        isAttackHighlighted = false;

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        foreach (var result in results) {
            if (click && result.gameObject.TryGetComponent(out AttackBase attack)) {
                battleManager.SelectAttack(attack);
                return;
            }
            if (result.gameObject.TryGetComponent(out Enemy enemy)) {
                if (click) {
                    battleManager.ApplyCurrentAttack(enemy, Input.mousePosition);
                }
                if (battleManager.IsAttackSelected()) {
                    battleManager.HighlightAttackedEnemies(enemy, Input.mousePosition);
                    isAttackHighlighted = true;
                }
                else {
                    float potentialDamage = enemy.GetPotentialDamage();
                    GameManager.Instance.gameData.SetPotentialDamageToPlayer(potentialDamage);
                }
                return;
            }
        }
        if (!isAttackHighlighted) battleManager.RemoveAttackHighlight();
    }
}
