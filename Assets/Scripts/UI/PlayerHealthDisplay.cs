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
        gameManager.player.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable() {
        
    }
    private void OnPlayerHealthChanged() {
        playerHealth.text = $"{(int)gameManager.player.Health}/{gameManager.player.MaxHealth}";
    }
}
