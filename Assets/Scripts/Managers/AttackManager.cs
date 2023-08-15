using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;



public class AttackManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameData gameData;
    [SerializeField] EnemyGrid enemyGrid;


    public void StartBattle() {
        gameData.ResetPlayerHealth(10, 10);
    }
}
