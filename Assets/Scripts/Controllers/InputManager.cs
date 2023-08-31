using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public BattleManager battleManager;
    public LevelManager levelManager;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    private bool overEnemy;

    private Item carryingItem;

    private event Action<List<RaycastResult>> handleInput;

    private void Start() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        GameManager.Instance.StateChanged += GameStateChanged;
        handleInput = HandleDefaultInput;
    }

    private void GameStateChanged(GameState state) {
        battleManager.HideEstimatedHealth();
        switch (state) {
            case GameState.Default:
                handleInput = HandleDefaultInput; break;
            case GameState.PlayingLevel:
                handleInput = HandleBattleInput; break;
            case GameState.Fighting:
                handleInput = HandleFightingInput; break;
        }
    }

    private void Update() {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        var raycastResults = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, raycastResults);

        handleInput(raycastResults);
    }

    private void HandleFightingInput(List<RaycastResult> raycastResults) {
        // no input processing during a fight
    }

    private void HandleDefaultInput(List<RaycastResult> raycastResults) {
        bool click = Input.GetMouseButtonDown(0);
        if (!click) return;

        foreach (var result in raycastResults) {
            if (carryingItem != null) {
                if (result.gameObject.TryGetComponent(out ItemContainer container)) {
                    carryingItem = container.PutOrSwapItem(carryingItem);
                    break;
                }
                if (result.gameObject.TryGetComponent(out RecyclebinManager rm)) {
                    Destroy(carryingItem.gameObject);
                    carryingItem = null;
                    break;
                }
            }
            else {
                if (result.gameObject.TryGetComponent(out Level level)) {
                    levelManager.HandleLevelSelected(level);
                    break;
                }
                if (result.gameObject.TryGetComponent(out ItemContainer container)) {
                    carryingItem = container.TakeItem();
                    break;
                }
                if (result.gameObject.TryGetComponent(out RestButtonHandler _)) {
                    GameManager.Instance.player.ResetHealth();
                }
            }
        }
    }

    private void HandleBattleInput(List<RaycastResult> raycastResults) { 
        if (Input.GetKeyDown(KeyCode.Escape)) {
            battleManager.DeselectAttack();
            return;
        }

        bool click = Input.GetMouseButtonDown(0);
        overEnemy = false;

        foreach (var result in raycastResults) {
            // if over an attack and clicked
            if (click && result.gameObject.TryGetComponent(out AttackBase attack)) {
                battleManager.SelectAttack(attack);
                return;
            }

            // if over an enemy
            if (result.gameObject.TryGetComponent(out Cell cell)) {
                overEnemy = true;
                if (battleManager.IsAttackSelected()) {
                    battleManager.HideEstimatedHealth();
                    battleManager.HighlightAttackedEnemies(cell, Input.mousePosition);
                    if (click) {
                        battleManager.ApplyCurrentAttack(cell, Input.mousePosition);
                    }
                }
                else {
                    if (cell.enemy != null) {
                        battleManager.DisplayEstimatedPlayerHealthAfterFight(cell.enemy);
                        if (click) {
                            battleManager.Fight(cell.enemy);
                        }
                    }
                }
                return;
            }
        }
        if (!overEnemy) {
            battleManager.RemoveAttackHighlight();
            battleManager.HideEstimatedHealth();
            if (click) {
                battleManager.DeselectAttack();
            }
        }
    }
}
