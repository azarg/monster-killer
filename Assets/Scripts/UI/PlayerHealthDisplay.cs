using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] Text playerHealth;
    [SerializeField] GameData gameData;

    private void Start() {
        gameData.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged() {
        playerHealth.text = $"{(int)gameData.playerHealth}/{gameData.maxPlayerHealth}";
    }
}
