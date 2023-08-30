using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerRemainingHealthChanged;
    public event Action OnPlayerStatsChanged;

    [SerializeField] private Stats baseStats = new() { attack = 25f, defense = 10f, magic = 50f, health = 120f };


    public EquipItemContainer[] equipment;

    public Stats stats;

    private float _remaining_health;
    public float remaining_health {
        get {
            return _remaining_health;
        }
        private set {
            _remaining_health = value;
            OnPlayerRemainingHealthChanged?.Invoke();
        }
    }

    private void Start() {
        remaining_health = baseStats.health;
        RecalculateStats();
    }

    public void ResetHealth() {
        remaining_health = stats.health;
    }

    public void Hurt(float damage) {
        this.remaining_health -= EstimatedHurt(damage);
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
        // start with base stats
        stats.attack = baseStats.attack;
        stats.defense = baseStats.defense;
        stats.magic = baseStats.magic;
        stats.health = baseStats.health;

        // add equipment stats
        foreach (EquipItemContainer container in equipment) {
            if (container.item != null) {
                var equipItem = (EquipItem)container.item;
                stats.attack += equipItem.stats.attack;
                stats.defense += equipItem.stats.defense;
                stats.magic += equipItem.stats.magic;
                stats.health += equipItem.stats.health;
            }
        }
        OnPlayerStatsChanged?.Invoke();
    }
}
