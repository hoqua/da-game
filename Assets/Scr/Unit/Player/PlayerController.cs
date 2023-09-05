using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private int maxMoveDistance = 1;

  private ActionComponent _actionComponent;
  private bool isPlayerTurn;

  private void Awake() {
    _actionComponent = GetComponent<ActionComponent>();
    GameManager.OnGameStateChanged += OnGameStateChanged;
  }

  private void Start() {
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, currentPosition);
  }

  private async void Update() {
    if (!isPlayerTurn) return;
    if (!Input.GetMouseButtonDown(0)) return;

    var position = LevelGrid.Instance.GetPosition(MouseWorld.GetPosition());
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);

    if (!LevelGrid.Instance.IsValidPosition(position, currentPosition, GetMoveDistance())) {
      return;
    }

    var cellOccupant = LevelGrid.Instance.GetOccupant(position);

    if (cellOccupant is not null && cellOccupant.GetComponent<EnemyController>()) {
      await _actionComponent.Attack(position);
    }
    else {
      await _actionComponent.Move(position);
    }

    OnPlayerTurnEnded?.Invoke();
  }

  private void OnDestroy() {
    GameManager.OnGameStateChanged -= OnGameStateChanged;
  }

  public static event Action OnPlayerTurnEnded;

  private void OnGameStateChanged(GameState gameState) {
    isPlayerTurn = gameState == GameState.PlayerTurn;
  }

  public int GetMoveDistance() {
    return maxMoveDistance;
  }

  public GridPosition GetPosition() {
    return LevelGrid.Instance.GetPosition(transform.position);
  }
}

internal enum PlayerState {
  WaitingInput,
  Moving,
  Attacking
}