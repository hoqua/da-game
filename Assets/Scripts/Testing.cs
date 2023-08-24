using System;
using UnityEngine;

public class Testing: MonoBehaviour {
  
  [SerializeField] private Transform debugPrefab;
  private GridSystem gridSystem;
  private void Start() {
    gridSystem = new GridSystem(10, 10, 2f);
    gridSystem.CreateDebugObjects(debugPrefab);
  }

  private void Update() {
    Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
  }
}