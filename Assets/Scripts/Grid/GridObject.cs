using UnityEngine;

public class GridObject {
  private GridPosition position;
  private GridSystem gridSystem;
  private Unit unit;

  public GridObject(GridSystem gridSystem, GridPosition position) {
    this.gridSystem = gridSystem;
    this.position = position;
  }

  public override string ToString() {
    return position.ToString() + "\n " + unit;
  }

  public void SetUnit(Unit unit) {
    this.unit = unit;
  }

  public Unit GetUnit() {
    return this.unit;
  }
}