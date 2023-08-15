using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int row;
    public int col;
    public Cell(int row, int col) {
        this.row = row;
        this.col = col;
    }
}

public abstract class AttackBase
{
    public abstract List<EnemyController> GetAttackedEnemies(EnemyController enemy, EnemyController[,] enemies, Vector3 mousePosition);
}