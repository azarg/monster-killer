using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameData gameData;

    public float GetHPS(float hit) {
        // TODO: incorporate player stats
        return hit;
    }

    public float GetDPS() {
        return 1f;
    }

    public float GetHealth() {
        return gameData.playerHealth;
    }

    public void Hurt(float damage) {
        gameData.playerHealth -= damage;
    }
}
