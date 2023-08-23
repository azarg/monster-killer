using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour
{
    public bool isCompleted;
    public int rows;
    public int columns;
    public EnemyType[] enemyTypes;

    [SerializeField] GameObject completedDisplay;
    [SerializeField] GameObject currentDisplay;

    internal void MarkAsCompleted() {
        completedDisplay.SetActive(true);
        currentDisplay.SetActive(false);
    }

    internal void MarkAsCurrent() {
        completedDisplay.SetActive(false);
        currentDisplay.SetActive(true);
    }

    internal void MarkAsHidden() {
        completedDisplay.SetActive(false);
        currentDisplay.SetActive(false);
    }
}
