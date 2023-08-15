using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttack : AttackBase
{
    public override List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] grid, Vector3 mousePosition) {
        
        Debug.Log("chain attack");
        return new List<EnemyController>();
    }
}
