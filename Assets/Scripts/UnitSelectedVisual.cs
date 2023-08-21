using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour {
  // Start is called before the first frame update
  [SerializeField] private Unit unit;
  
  private MeshRenderer meshRenderer;
  private void Awake() {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  private void Start() {
    UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
  }
  
  private void OnSelectedUnitChanged(object sender, EventArgs e) {
    Debug.Log(UnitActionSystem.Instance.GetSelectedUnit());
    meshRenderer.enabled = UnitActionSystem.Instance.GetSelectedUnit() == unit;
  }
}