using System;
using System.Collections.Generic;
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

  public void AddUnit(HeroController heroController, GridPosition position) {
    var gridObject = gridSystem.GetGridObject(position);
    gridObject.AddUnit(heroController);
  }

  public List<HeroController> GetUnits(GridPosition position) {
    var gridObject = gridSystem.GetGridObject(position);
    return gridObject.GetUnitList();
  }

  private Vector3 GetUnitPosition(GridPosition gridPosition) {
    return new Vector3(gridPosition.x, 0, gridPosition.z) * 2f;
  }

  public void RemoveUnit(GridPosition position, HeroController heroController) {
    var gridObject = gridSystem.GetGridObject(position);
    gridObject.RemoveUnit(heroController);
  }

  public GridPosition GetPosition(Vector3 position) {
    return gridSystem.GetGridPosition(position);
  }

  public Vector3 GetWorldPosition(GridPosition position) {
    return gridSystem.GetWorldPosition(position);
  }

  public void UnitMoved(HeroController heroController, GridPosition oldPosition, GridPosition newPosition) {
    RemoveUnit(oldPosition, heroController);
    AddUnit(heroController, newPosition);
  }

  public bool IsValidGridPosition(GridPosition gridPosition) {
    return gridSystem.IsValidGridPosition(gridPosition);
  }


  public bool HasUnits(GridPosition gridPosition) {
    return gridSystem.GetGridObject(gridPosition).HasUnits();
  }

  public List<GridPosition> GetValidMovePositions(GridPosition unitPosition, int maxMoveDistance) {
    var validMovePositions = new List<GridPosition>();

    for (var x = -maxMoveDistance; x <= maxMoveDistance; x++) {
      for (var z = -maxMoveDistance; z <= maxMoveDistance; z++) {
        var testGridPosition = new GridPosition(unitPosition.x + x, unitPosition.z + z);

        if (
          !IsValidGridPosition(testGridPosition) ||
          GridPosition.isEquals(unitPosition, testGridPosition) ||
          HasUnits(testGridPosition)
        ) {
          continue;
        }

        Debug.Log(testGridPosition);
        validMovePositions.Add(testGridPosition);
      }
    }

    return validMovePositions;
  }

  public bool IsValidPosition(GridPosition targetPosition, GridPosition unitPosition, int maxMoveDistance) {
    var validPositions = GetValidMovePositions(unitPosition, maxMoveDistance);
    return validPositions.Contains(targetPosition);
  }
}