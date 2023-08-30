using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] Text playerHealth;
    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
        gameManager.player.OnPlayerRemainingHealthChanged += OnPlayerHealthChanged;
        gameManager.player.OnPlayerStatsChanged += OnPlayerHealthChanged;
    }

    private void OnDisable() {
        gameManager.player.OnPlayerRemainingHealthChanged -= OnPlayerHealthChanged;
        gameManager.player.OnPlayerStatsChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged() {
        playerHealth.text = $"{(int)Mathf.Max(1f, gameManager.player.remaining_health)}/{gameManager.player.stats.health}";
    }
}
