using UnityEngine;

public class GridSystem {
  private readonly int _width;
  private readonly int _height;
  private readonly float _cellSize;
  private readonly GridObject[,] _gridObjects = new GridObject[10, 10];

  public GridSystem(int width, int height, float cellSize) {
    this._width = width;
    this._height = height;
    this._cellSize = cellSize;

    for (var x = 0; x < width; x++) {
      for (var z = 0; z < height; z++) {
        var position = new GridPosition(x, z);
        _gridObjects[x, z] = new GridObject(position);
      }
    }
  }

  public Vector3 GetWorldPosition(GridPosition gridPosition) {
    return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
  }

  public GridPosition GetGridPosition(Vector3 gridPosition) {
    return new GridPosition(
      Mathf.RoundToInt(gridPosition.x / _cellSize),
      Mathf.RoundToInt(gridPosition.z / _cellSize)
    );
  }

  public void CreateDebugObjects(Transform debugPrefab) {
    for (var x = 0; x < _width; x++) {
      for (var z = 0; z < _height; z++) {
        var gridPosition = new GridPosition(x, z);
        var debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
        var debugObject = debugTransform.GetComponent<GridDebugObject>();
        debugObject.SetGridObject(GetGridObject(gridPosition));
      }
    }
  }

  public GridObject GetGridObject(GridPosition gridPosition) {
    return _gridObjects[gridPosition.x, gridPosition.z];
  }

  public bool IsValidGridPosition(GridPosition gridPosition) {
    return gridPosition.x >= 0 &&
           gridPosition.z >= 0 &&
           gridPosition.x < _width &&
           gridPosition.z < _height;
  }
}