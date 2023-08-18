using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public event Action OnPlayerHealthChanged;
    public event Action OnEstimatedHealthChanged;

    public Enemy[,] enemies;

    public float playerHealth;
    public float maxPlayerHealth;
    public float estimatedPlayerHealthAfterFight;

    private void OnEnable() {
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged = null;
    }


    public void SetPlayerHealth(float amount) {
        playerHealth = amount;
        OnPlayerHealthChanged?.Invoke();

    }
    public void ChangePlayerHealth(float amount) {
        playerHealth += amount;
        OnPlayerHealthChanged?.Invoke();
    }


    public float GetDPS() {
        return 1f;
    }
}
