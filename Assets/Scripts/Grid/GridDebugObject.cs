using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour {
  [SerializeField] private TextMeshPro textMesh;
  private GridObject gridObject;

  public GridDebugObject(GridObject gridObject) {
    this.gridObject = gridObject;
  }

  public void SetGridObject(GridObject gridObject) {
    this.gridObject = gridObject;
    //textMesh.SetText(gridObject.GetGridPosition().ToString());
  }

  private void Update() {
    textMesh.text = gridObject.ToString();
  }
}