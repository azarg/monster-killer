using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyType : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public Sprite sprite;
    public float startingHealth;
    public float baseDamage;
}