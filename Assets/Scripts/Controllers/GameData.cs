using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public event Action OnPlayerHealthChanged;
    public event Action OnPotentialDamageToPlayerChanged;

    public Enemy[,] enemies;

    public int playerHealth;
    public int maxPlayerHealth;
    public float potentialDamageToPlayer;

    private void OnEnable() {
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged = null;
        OnPotentialDamageToPlayerChanged = null;
    }

    public void SetPotentialDamageToPlayer(float potentialDamage) {
        potentialDamageToPlayer = potentialDamage;
        OnPotentialDamageToPlayerChanged?.Invoke();
    }

    public void ResetPlayerHealth(int health, int maxHealth) {
        playerHealth = health;
        maxPlayerHealth = maxHealth;
        OnPlayerHealthChanged?.Invoke();
    }

    public void ChangePlayerHealth(int amount) {
        playerHealth += amount;
        OnPlayerHealthChanged?.Invoke();
    }

    public void ChangeMaxPlayerHealth(int amount) {
        maxPlayerHealth += amount;
        OnPlayerHealthChanged?.Invoke();
    }
}
