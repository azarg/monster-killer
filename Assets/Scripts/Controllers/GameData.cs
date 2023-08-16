using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public event Action OnPlayerHealthChanged;
    
    public int playerHealth;
    public int maxPlayerHealth;
    public AttackBase currentAttack;

    private void OnEnable() {
        OnPlayerHealthChanged = null;
    }

    public void SetCurrentAttack(AttackBase attack) {
        currentAttack = attack;
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
