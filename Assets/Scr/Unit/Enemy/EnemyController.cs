using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  public static readonly string tag = "Enemy";

  private ActionComponent _actionComponent;

  private void Start() {
    _actionComponent = GetComponent<ActionComponent>();
  }

  public async Task Attack(GridPosition newPosition) {
    await _actionComponent.Attack(newPosition);
  }
}