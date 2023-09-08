using UnityEngine;

public class PositionerComponent : MonoBehaviour {
  private void Start() {
    var currentPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.AddToCell(this, currentPosition);
  }
}