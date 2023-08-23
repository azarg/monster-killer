using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Default, InBattle }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData gameData;
    public LevelManager levelManager;
    public GameState gameState;

    [SerializeField] RectTransform monsterPanel;
    [SerializeField] RectTransform attackPanel;
    [SerializeField] RectTransform attackPanel_default_position;
    [SerializeField] RectTransform attackPanel_battle_position;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeGameState(GameState state) {
        gameState = state;
        if (gameState == GameState.Default) {
            attackPanel.SetParent(attackPanel_default_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(false);
        } 
        if (gameState == GameState.InBattle) {
            attackPanel.SetParent(attackPanel_battle_position, worldPositionStays: false);
            monsterPanel.gameObject.SetActive(true);
        }
    }
}
