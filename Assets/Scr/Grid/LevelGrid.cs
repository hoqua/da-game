using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LevelGrid : MonoBehaviour {
  public static LevelGrid Instance { get; private set; }

  [SerializeField] private Transform debugPrefab;

  private GridSystem _gridSystem;

  private void Awake() {
    if (Instance != null) {
      Debug.Log("There should never be two level grids");
      Destroy(gameObject);
    }

    Instance = this;
    _gridSystem = new GridSystem(10, 10, 2f);
    _gridSystem.CreateDebugObjects(debugPrefab);
  }

  public void AddUnit(MonoBehaviour occupant, GridPosition position) {
    var gridObject = _gridSystem.GetGridObject(position);
    gridObject.AddOccupant(occupant);
  }

  public MonoBehaviour GetUnits(GridPosition position) {
    return _gridSystem.GetGridObject(position).GetOccupant();
  }

  private Vector3 GetUnitPosition(GridPosition gridPosition) {
    return new Vector3(gridPosition.x, 0, gridPosition.z) * 2f;
  }

  public void RemoveUnit(GridPosition position, MonoBehaviour occupant) {
    _gridSystem.GetGridObject(position).RemoveOccupant();
  }

  public GridPosition GetPosition(Vector3 position) {
    return _gridSystem.GetGridPosition(position);
  }

  public Vector3 GetWorldPosition(GridPosition position) {
    return _gridSystem.GetWorldPosition(position);
  }

  public void UnitMoved(MonoBehaviour occupant, GridPosition oldPosition, GridPosition newPosition) {
    RemoveUnit(oldPosition, occupant);
    AddUnit(occupant, newPosition);
  }

  public bool IsValidGridPosition(GridPosition gridPosition) {
    return _gridSystem.IsValidGridPosition(gridPosition);
  }


  public bool HasUnits(GridPosition gridPosition) {
    return _gridSystem.GetGridObject(gridPosition).IsOccupied();
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
    if(!IsValidGridPosition(targetPosition)) return false;
    if (GridPosition.isEquals(unitPosition, targetPosition)) return false;
    
    return true;
  }
  
  public MonoBehaviour GetOccupant(GridPosition gridPosition) {
    return _gridSystem.GetGridObject(gridPosition).GetOccupant();
  }
}