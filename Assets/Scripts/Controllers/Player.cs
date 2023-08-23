using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    private void OnEnable() {
        gameManager = GameManager.Instance;
    }
    public float GetHPS(float hit) {
        // TODO: incorporate player stats
        return hit;
    }

    public float GetDPS() {
        return 1f;
    }

    public float GetHealth() {
        return gameManager.playerHealth;
    }

    public void Hurt(float damage) {
        gameManager.playerHealth -= damage;
    }
}
