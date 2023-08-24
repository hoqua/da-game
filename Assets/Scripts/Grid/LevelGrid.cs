using System;
using UnityEngine;

public class LevelGrid : MonoBehaviour {
  public static LevelGrid Instance { get; private set; }

  [SerializeField] private Transform debugPrefab;

  private GridSystem gridSystem;

  private void Awake() {
    if (Instance != null) {
      Debug.Log("There should never be two level grids");
      Destroy(gameObject);
    }

    Instance = this;
    gridSystem = new GridSystem(10, 10, 2f);
    gridSystem.CreateDebugObjects(debugPrefab);
  }

  public void SetUnit(Unit unit, GridPosition position) {
    var gridObject = gridSystem.GetGridObject(position);
    gridObject.SetUnit(unit);
  }

  private Vector3 GetUnitPosition(GridPosition gridPosition) {
    return new Vector3(gridPosition.x, 0, gridPosition.z) * 2f;
  }

  public void RemoveUnit(GridPosition position) {
    var gridObject = gridSystem.GetGridObject(position);
    gridObject.SetUnit(null);
  }

  public GridPosition GetPosition(Vector3 position) {
    return gridSystem.GetGridPosition(position);
  }
  
  public void UnitMoved(Unit unit, GridPosition oldPosition, GridPosition newPosition) {
    gridSystem.GetGridObject(oldPosition).SetUnit(null);
    gridSystem.GetGridObject(newPosition).SetUnit(unit);
  }
}