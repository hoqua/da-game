using System;
using UnityEngine;

public class MouseWorld : MonoBehaviour {
  [SerializeField] private LayerMask mousePlaneMask;
  
  private static MouseWorld instance;

  private void Awake() {
    if (instance != null) {
      Destroy(gameObject);
      return;
    }
    
    instance = this;
  }
  
    
  public static Vector3 GetPosition() {
    // TODO: fix duplicated code on unit
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Physics.Raycast(ray, out var raycastHit, float.MaxValue, instance.mousePlaneMask); 
    return raycastHit.point;  
  }


  private void Update(){
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePlaneMask); 
    transform.position = raycastHit.point;
  }
}
