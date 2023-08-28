using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class GridObject {
  private GridPosition _position;
  [CanBeNull] private MonoBehaviour _occupant;

  public GridObject(GridPosition position) {
    this._position = position;
  }

  public override string ToString() {
    return _position.ToString() + "\n " + _occupant;
  }

  public void AddOccupant(MonoBehaviour occupant) {
    this._occupant = occupant;
  }

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