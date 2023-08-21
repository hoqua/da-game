using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour {
  public static UnitActionSystem Instance { get; private set; }

  [SerializeField] private LayerMask unitLayerMask;
  [SerializeField] private Unit selectedUnit;

  public event EventHandler OnSelectedUnitChanged;

  private void Awake() {
    // this script is attached to script execution order

    if (Instance != null) {
      Debug.LogError("There should never be two unit action systems");
      //Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      if (TrySelectUnit()) return;
      selectedUnit.Move(MouseWorld.GetPosition());
    }
  }

  private bool TrySelectUnit() {
    // TOTO: refactor 
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitLayerMask)) {
      if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)) {
        Debug.Log("SELECTED");
        SetSelectedUnit(unit);

        return true;
      }
    }

    return false;
  }

  private void SetSelectedUnit(Unit unit) {
    selectedUnit = unit;
    OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
  }

  public Unit GetSelectedUnit() {
    return selectedUnit;
  }
}