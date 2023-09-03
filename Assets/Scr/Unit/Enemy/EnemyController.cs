using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  private ActionComponent _actionComponent;
  private GridPosition _currentPosition;

  private void Start() {
    _actionComponent = GetComponent<ActionComponent>();
    _currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddUnit(this, _currentPosition);
  }

  public async Task Attack(GridPosition newPosition) {
    await _actionComponent.Attack(newPosition);
  }
}