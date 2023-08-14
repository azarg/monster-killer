using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameData gameData;

    public void StartBattle() {
        gameData.ResetPlayerHealth(10,10);
    }
}
