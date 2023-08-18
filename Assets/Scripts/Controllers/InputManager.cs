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

    private bool overEnemy;

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
        overEnemy = false;

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        foreach (var result in results) {
            // if over an attack and clicked
            if (click && result.gameObject.TryGetComponent(out AttackBase attack)) {
                battleManager.SelectAttack(attack);
                return;
            }

            // if over an enemy
            if (result.gameObject.TryGetComponent(out Enemy enemy)) {
                overEnemy = true;
                if (battleManager.IsAttackSelected()) {
                    battleManager.HideEstimatedHealth();
                    battleManager.HighlightAttackedEnemies(enemy, Input.mousePosition);
                    if (click) {
                        battleManager.ApplyCurrentAttack(enemy, Input.mousePosition);
                    }
                }
                else {
                    battleManager.DisplayEstimatedPlayerHealthAfterFight(enemy);
                    if (click) {
                        battleManager.Fight(enemy);
                    }
                }
                return;
            }
        }
        if (!overEnemy) {
            battleManager.RemoveAttackHighlight();
            battleManager.HideEstimatedHealth();
        }
    }
}
