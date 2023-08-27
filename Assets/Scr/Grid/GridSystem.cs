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

  public Vector3 GetWorldPosition(GridPosition gridPosition) {
    return new Vector3(gridPosition.x, 0,gridPosition.z) * cellSize;
  }

  public GridPosition GetGridPosition(Vector3 gridPosition) {
    return new GridPosition(
      Mathf.RoundToInt(gridPosition.x / cellSize),
      Mathf.RoundToInt(gridPosition.z / cellSize)
    );
  }

  public void CreateDebugObjects(Transform debugPrefab) {
    for (var x = 0; x < width; x++) {
      for (var z = 0; z < height; z++) {
        var gridPosition = new GridPosition(x, z);
        var debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
        var debugObject = debugTransform.GetComponent<GridDebugObject>();
        debugObject.SetGridObject(GetGridObject(gridPosition));
      }
    }
  }
  
  public GridObject GetGridObject(GridPosition gridPosition) {
    return gridObjects[gridPosition.x, gridPosition.z];
  }
  
  public bool IsValidGridPosition(GridPosition gridPosition) {
    return gridPosition.x >= 0 && 
           gridPosition.z >= 0 && 
           gridPosition.x < width &&
           gridPosition.z < height;
  }
  
  
}