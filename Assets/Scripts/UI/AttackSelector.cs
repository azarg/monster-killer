using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameData gameData;
    private AttackBase attackType;

    private void Awake() {
        attackType = gameObject.GetComponent<AttackBase>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        gameData.SetCurrentAttack(attackType);
    }
}
