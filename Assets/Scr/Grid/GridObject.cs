using JetBrains.Annotations;
using UnityEngine;

public class GridObject {
  private readonly GridPosition _position;

  [CanBeNull]
  private MonoBehaviour _occupant;

  public GridObject(GridPosition position) {
    _position = position;
  }

  public override string ToString() {
    return _position + "\n " + _occupant;
  }

  public void AddOccupant(MonoBehaviour occupant) {
    _occupant = occupant;
  }

  [CanBeNull]
  public MonoBehaviour GetOccupant() {
    Debug.Log(ToString());
    return _occupant;
  }

  public void RemoveOccupant() {
    _occupant = null;
  }

  public bool IsOccupied() {
    return _occupant is null;
  }
}