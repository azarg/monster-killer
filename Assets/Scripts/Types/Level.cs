using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public int id;
    public int rows;
    public int columns;
    public Enemy[] enemies;
}
