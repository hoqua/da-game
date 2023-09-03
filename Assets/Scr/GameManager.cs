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

  private async Task Start() {
    await UpdateGameState(GameState.PlayerTurn);
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    PlayerController.OnPlayerTurnEnded += OnPlayerTurnEnded;
  }

  private void OnDestroy() {
    PlayerController.OnPlayerTurnEnded -= OnPlayerTurnEnded;
  }

  public static event Action<GameState> OnGameStateChanged;

  private async Task UpdateGameState(GameState gameState) {
    // Event on top level because Tasks(EnemyAttacks) are awaited somehow
    // Moved to explicit await for all async methods
    OnGameStateChanged?.Invoke(gameState);

    if (gameState == GameState.EnemyTurn) {
      await EnemyAttacks();
    }

    if (gameState == GameState.GameOver) {
      Debug.Log("Game Over");
    }
  }

  private async Task EnemyAttacks() {
    var enemies = LevelGrid.Instance.GetNeighboringEnemies(_player.GetPosition());

    var enemyAttacks = new List<Task>();
    foreach (var enemy in enemies) {
      enemyAttacks.Add(enemy.Attack(_player.GetPosition()));
    }

    await Task.WhenAll(enemyAttacks);
    await UpdateGameState(GameState.PlayerTurn);
  }

  private async void OnPlayerTurnEnded() {
    await UpdateGameState(GameState.EnemyTurn);
  }
}

public enum GameState {
  PlayerTurn,
  EnemyTurn,
  GameOver
}