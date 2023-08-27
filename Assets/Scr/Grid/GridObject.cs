using System.Collections.Generic;
using System.Linq;

public class GridObject {
  private GridPosition position;
  private GridSystem gridSystem;
  private List<HeroController> unitList;

  public GridObject(GridSystem gridSystem, GridPosition position) {
    this.gridSystem = gridSystem;
    this.position = position;
    unitList = new List<HeroController>();
  }

  public override string ToString() {
    var units = unitList.Aggregate("", (current, unit) => current + (unit.ToString() + "\n"));
    return position.ToString() + "\n " + units;
  }

  public void AddUnit(HeroController heroController) {
    unitList.Add(heroController);
  }

  public void RemoveUnit(HeroController heroController) {
    unitList.Remove(heroController);
  }

  public List<HeroController> GetUnitList() {
    return unitList;
  }
  
  public bool HasUnits() {
    return unitList.Count > 0;
  }
}