using UnityEngine;

public class HeroController : MonoBehaviour {
  [SerializeField] private int maxMoveDistance = 1;
  private bool _finishedAttack = true;
  private bool _finishedMove = true;

  private MovementSystem _movementSystem;

  private void Awake() {
    _movementSystem = GetComponent<MovementSystem>();
  }

  private void Start() {
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, currentPosition);
  }

  private async void Update() {
    if (!_finishedAttack || !_finishedMove) return;
    if (!Input.GetMouseButtonDown(0)) return;

    var position = LevelGrid.Instance.GetPosition(MouseWorld.GetPosition());
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);

    if (!LevelGrid.Instance.IsValidPosition(position, currentPosition, GetMoveDistance())) {
      return;
    }

    var cellOccupant = LevelGrid.Instance.GetOccupant(position);

    if (cellOccupant is not null && cellOccupant.GetComponent<EnemyController>()) {
      _finishedAttack = false;
      _finishedAttack = await _movementSystem.Attack(position);
    }
    else {
      _finishedMove = false;
      _finishedMove = await _movementSystem.Move(position);
    }
  }

  public int GetMoveDistance() {
    return maxMoveDistance;
  }
}