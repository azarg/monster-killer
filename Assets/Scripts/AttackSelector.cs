using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSelector : MonoBehaviour, IPointerClickHandler
{
    private AttackBase attackType;

    private void Awake() {
        attackType = gameObject.GetComponent<AttackBase>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        AttackManager.Instance.SetCurrentAttack(attackType);
    }
}
