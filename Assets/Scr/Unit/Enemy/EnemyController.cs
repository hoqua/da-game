using UnityEngine;

public class EnemyController : MonoBehaviour {
  private GridPosition _currentPosition;

  private void Start() {
    _currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, _currentPosition);
  }
}