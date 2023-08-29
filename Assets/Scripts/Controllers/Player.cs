using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerHealthChanged;
    public event Action OnPlayerStatsChanged;

    [SerializeField] private Stats baseStats = new() { attack = 25f, defense = 10f, magic = 50f, health = 120f };


    public EquipItemContainer[] equipment;

    public Stats stats;

    private float health;
    public float Health {
        get {
            return health;
        }
        private set {
            health = value;
            OnPlayerHealthChanged?.Invoke();
        }
    }

    private float maxHealth;
    public float MaxHealth {
        get {
            return maxHealth;
        }
        private set {
            maxHealth = value;
            OnPlayerHealthChanged?.Invoke();
        }
    }

    private void Start() {
        Health = baseStats.health;
        RecalculateStats();
    }

    public void ResetHealth() {
        //TODO: max health should depend on stats;
        MaxHealth = baseStats.health;
        Health = MaxHealth;
    }

    public void Hurt(float damage) {
        this.Health -= EstimatedHurt(damage);
    }

    /// <summary>
    /// Returns how much the player will be hurt (health reduced)
    /// for a given enemy damage
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public float EstimatedHurt(float damage) {
        float defense = baseStats.defense;
        foreach (ItemContainer container in equipment) {
            if (container.item != null) {
                var equipItem = (EquipItem)container.item;
                defense += equipItem.stats.defense;
            }
        }
        damage *= Mathf.Pow(1.05f, -defense) + 0.2f;
        return damage;
    }

    /// <summary>
    /// Returns how much damage the player will cause to an enemy
    /// in a 1 on 1 fight (not magic)
    /// </summary>
    /// <returns></returns>
    public float EstimatedDamage() {
        float attack = baseStats.attack;
        foreach(ItemContainer container in equipment) {
            if(container.item != null) {
                var equipItem = (EquipItem)container.item;
                attack += equipItem.stats.attack;
            }
        }
        return attack;
    }

    public void RecalculateStats() {
        //Debug.Log("recalculating stats");
        // start with base stats
        stats.attack = baseStats.attack;
        stats.defense = baseStats.defense;
        //Debug.Log($"base defense = {stats.defense}");
        stats.magic = baseStats.magic;

        // add equipment stats
        foreach (EquipItemContainer container in equipment) {
            //Debug.Log($"checking container - {container}, item - {container.item}");
            if (container.item != null) {
                var equipItem = (EquipItem)container.item;
                stats.attack += equipItem.stats.attack;
                stats.defense += equipItem.stats.defense;
                Debug.Log($"defense += {equipItem.stats.defense}");
                stats.magic += equipItem.stats.magic;
            }
        }
        //Debug.Log($"stats.defense = {stats.defense}");
        OnPlayerStatsChanged?.Invoke();
    }
}
