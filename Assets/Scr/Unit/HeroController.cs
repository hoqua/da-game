using System;
using UnityEngine;

public class HeroController : MonoBehaviour {
  [SerializeField] private Animator unitAnimator;
  [SerializeField] private int maxMoveDistance = 1;

  private GridPosition _currentPosition;
  private MovementSystem _movementSystem;

  private void Awake() {
    _movementSystem = GetComponent<MovementSystem>();
  }

  private void Start() {
    _currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, _currentPosition);
  }

  private void Update() {
    if (!Input.GetMouseButtonDown(0)) return;
    
    var position = LevelGrid.Instance.GetPosition(MouseWorld.GetPosition());

    if (!LevelGrid.Instance.IsValidPosition(position, _currentPosition, maxMoveDistance)) {
      return;
    }

    var cellOccupant = LevelGrid.Instance.GetOccupant(position);

    if (cellOccupant is not null && cellOccupant.GetComponent<EnemyController>()) {
      _movementSystem.MeleeAttack(position);
    }else {
      _movementSystem.Move(position);
    }


  }
}