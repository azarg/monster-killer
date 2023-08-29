using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] Text attack;
    [SerializeField] Text defense;
    [SerializeField] Text magic;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
        gameManager.player.OnPlayerStatsChanged += OnPlayerStatsChanged;
    }
    private void OnDisable() {
        gameManager.player.OnPlayerStatsChanged -= OnPlayerStatsChanged;
    }

    private void OnPlayerStatsChanged() {
        var stats = gameManager.player.stats;
        attack.text = $"{(int)stats.attack}";
        defense.text = $"{(int)stats.defense}";
        magic.text = $"{(int)stats.magic}";
    }
}
