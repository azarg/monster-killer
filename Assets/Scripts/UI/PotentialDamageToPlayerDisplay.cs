using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotentialDamageToPlayerDisplay : MonoBehaviour
{
    [SerializeField] Text potentialDamage;
    [SerializeField] GameData gameData;

    private void Start() {
        gameData.OnPotentialDamageToPlayerChanged += OnPotentialDamageToPlayerChanged;
    }

    private void OnPotentialDamageToPlayerChanged() {
        int remainingHealth = gameData.playerHealth - (int)gameData.potentialDamageToPlayer;
        if (remainingHealth > 0) {
            potentialDamage.text = $"{remainingHealth}";
        }
        else {
            potentialDamage.text = "---";
        }
    }
}
