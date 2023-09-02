using JetBrains.Annotations;
using UnityEngine;

public class GridObject {
  [CanBeNull] private MonoBehaviour _occupant;
  private GridPosition _position;

  public GridObject(GridPosition position) {
    this._position = position;
  }

  public override string ToString() {
    return _position.ToString() + "\n " + _occupant;
  }

  public void AddOccupant(MonoBehaviour occupant) {
    this._occupant = occupant;
  }

  [CanBeNull]
  public MonoBehaviour GetOccupant() {
    return _occupant;
  }

  public void RemoveOccupant() {
    this._occupant = null;
  }

  public bool IsOccupied() {
    return _occupant is null;
  }
}