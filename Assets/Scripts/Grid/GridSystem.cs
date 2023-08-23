using UnityEngine;

public class GridSystem {
  private int width;
  private int height;
  private float cellSize;
  private GridObject[,] gridObjects = new GridObject[10,10];

  public GridSystem(int width, int height, float cellSize) {
    this.width = width;
    this.height = height;
    this.cellSize = cellSize;

    for (int x = 0; x < width; x++) {
      for (int z = 0; z < height; z++) {
        var position = new GridPosition(x, z);
        gridObjects[x, z] = new GridObject(this, position);
      }
    }
  }

  private Vector3 GetWorldPosition(int x, int z) {
    return new Vector3(x, 0, z) * cellSize;
  }

  public GridPosition GetGridPosition(Vector3 gridPosition) {
    return new GridPosition(
      Mathf.FloorToInt(gridPosition.x / cellSize),
      Mathf.FloorToInt(gridPosition.z / cellSize)
    );
  }

  public void CreateDebugObjects(Transform debugPrefab) {
    for (var x = 0; x < width; x++) {
      for (var z = 0; z < height; z++) {
        GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
      }
    }
  }
}