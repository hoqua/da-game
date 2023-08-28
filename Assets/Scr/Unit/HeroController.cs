using System;
using UnityEngine;

public class HeroController : MonoBehaviour {
  [SerializeField] private Animator unitAnimator;
  [SerializeField] private int maxMoveDistance = 1;

  private MovementSystem _movementSystem;

  private void Awake() {
    _movementSystem = GetComponent<MovementSystem>();
  }

  private void Start() {
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, currentPosition);
  }

  private void Update() {
    if (!Input.GetMouseButtonDown(0)) return;

    var position = LevelGrid.Instance.GetPosition(MouseWorld.GetPosition());
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);

    if (!LevelGrid.Instance.IsValidPosition(position, currentPosition, maxMoveDistance)) {
      return;
    }

    var cellOccupant = LevelGrid.Instance.GetOccupant(position);

    if (cellOccupant is not null && cellOccupant.GetComponent<EnemyController>()) {
      _movementSystem.MeleeAttack(position);
    }
    else {
      _movementSystem.Move(position);
    }
  }
}