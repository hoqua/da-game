using System;
using UnityEngine;

public class HeroController : MonoBehaviour {
  [SerializeField] private Animator unitAnimator;
  [SerializeField] private int maxMoveDistance = 1;

  private GridPosition currentPosition;
  private MovementSystem _movementSystem;

  private void Awake() {
    _movementSystem = GetComponent<MovementSystem>();
  }

  private void Start() {
    currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, currentPosition);
  }

  private void Update() {
    if (Input.GetMouseButtonDown(0)) {
      var position = LevelGrid.Instance.GetPosition(MouseWorld.GetPosition());

      if (LevelGrid.Instance.IsValidPosition(position, currentPosition, maxMoveDistance)) {
        _movementSystem.MeleeAttack(position);
      }
    }

    var newGridPosition = LevelGrid.Instance.GetPosition(transform.position);
    
    if (!GridPosition.isEquals(newGridPosition, currentPosition)) {
      LevelGrid.Instance.UnitMoved(this, currentPosition, newGridPosition);
      currentPosition = newGridPosition;
    }
  }
}