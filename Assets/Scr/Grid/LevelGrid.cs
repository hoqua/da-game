using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelGrid : MonoBehaviour {
  [SerializeField] private Transform debugPrefab;

  private GridSystem _gridSystem;
  public static LevelGrid Instance { get; private set; }

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


  public void RemoveUnit(GridPosition position, MonoBehaviour occupant) {
    _gridSystem.GetGridObject(position).RemoveOccupant();
  }

  public GridPosition GetPosition(Vector3 position) {
    return _gridSystem.GetGridPosition(position);
  }

  public Vector3 GetWorldPosition(GridPosition position) {
    return _gridSystem.GetWorldPosition(position);
  }

  public void MoveUnit(GridPosition oldPosition, GridPosition newPosition) {
    var occupant = _gridSystem.GetGridObject(oldPosition).GetOccupant();
    RemoveUnit(oldPosition, occupant);
    AddUnit(occupant, newPosition);
  }

  public bool IsValidGridPosition(GridPosition gridPosition) {
    return _gridSystem.IsValidGridPosition(gridPosition);
  }


  // public bool HasUnits(GridPosition gridPosition) {
  //   return _gridSystem.GetGridObject(gridPosition).IsOccupied();
  // }

  // public List<GridPosition> GetValidMovePositions(GridPosition unitPosition, int maxMoveDistance) {
  //   var validMovePositions = new List<GridPosition>();
  //
  //   for (var x = -maxMoveDistance; x <= maxMoveDistance; x++) {
  //     for (var z = -maxMoveDistance; z <= maxMoveDistance; z++) {
  //       var testGridPosition = new GridPosition(unitPosition.x + x, unitPosition.z + z);
  //
  //       if (
  //         !IsValidGridPosition(testGridPosition) ||
  //         GridPosition.isEquals(unitPosition, testGridPosition) ||
  //         HasUnits(testGridPosition)
  //       ) {
  //         continue;
  //       }
  //
  //       Debug.Log(testGridPosition);
  //       validMovePositions.Add(testGridPosition);
  //     }
  //   }
  //
  //   return validMovePositions;
  // }

  // Diagonal resulting in a total Manhattan distance of 2.
  private int CellDistance(GridPosition cell1, GridPosition cell2) {
    int deltaX = Mathf.Abs(cell1.x - cell2.x);
    int deltaY = Mathf.Abs(cell1.z - cell2.z);

    return deltaX + deltaY;
  }

  public bool IsValidPosition(GridPosition targetPosition, GridPosition unitPosition, int moveDistance) {
    if (!IsValidGridPosition(targetPosition)) return false;
    if (GridPosition.isEquals(unitPosition, targetPosition)) return false;

    var distance = CellDistance(targetPosition, unitPosition);
    if (distance != moveDistance) return false;

    return true;
  }

  [CanBeNull]
  public MonoBehaviour GetOccupant(GridPosition gridPosition) {
    return _gridSystem.GetGridObject(gridPosition).GetOccupant();
  }

  public List<EnemyController> GetNeighboringEnemies(GridPosition gridPosition) {
    var enemiesList = new List<EnemyController>(); // Assuming 4 neighboring cells (top, bottom, left, right)

    // clockwise order from left bottom
    int[] xOffset = { -1, -1, -1, 0, 1, 1, 1, 0 };
    int[] zOffset = { -1, 0, 1, 1, 1, 0, -1, -1 };

    for (var i = 0; i < xOffset.Length; i++) {
      var newX = gridPosition.x + xOffset[i];
      var newZ = gridPosition.z + zOffset[i];

      // Check if the new coordinates are within the grid bounds
      var pickedPosition = new GridPosition(newX, newZ);

      if (!IsValidGridPosition(pickedPosition)) continue;

      var enemy = GetOccupant(pickedPosition)?.GetComponent<EnemyController>();
      if (enemy) {
        enemiesList.Add(enemy);
      }
    }

    return enemiesList;
  }
}