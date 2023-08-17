using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] Level level;

    public void OnPointerClick(PointerEventData eventData) {
        levelManager.StartLevel(level);
    }
}
