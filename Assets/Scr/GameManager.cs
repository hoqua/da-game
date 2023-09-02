using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {
  private PlayerController _player;
  public static GameManager Instance { get; private set; }

  private void Awake() {
    if (Instance != null) {
      Debug.Log("There should never be two game managers");
      Destroy(gameObject);
    }

    Instance = this;
  }

  private void Start() {
    UpdateGameState(GameState.PlayerTurn);
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
  }

  public static event Action<GameState> OnGameStateChanged;

  public void UpdateGameState(GameState gameState) {
    if (gameState == GameState.PlayerTurn) {
    }
    else if (gameState == GameState.EnemyTurn) {
      EnemyAttacks();
    }
    else if (gameState == GameState.GameOver) {
    }

    OnGameStateChanged?.Invoke(gameState);
  }

  private async void EnemyAttacks() {
    var enemies = LevelGrid.Instance.GetNeighboringEnemies(_player.GetPosition());

    var enemyAttacks = new List<Task>();
    foreach (var enemy in enemies) {
      enemyAttacks.Add(enemy.Attack(_player.GetPosition()));
    }

    await Task.WhenAll(enemyAttacks);
  }
}

public enum GameState {
  PlayerTurn,
  EnemyTurn,
  GameOver
}