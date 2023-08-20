using System;
using UnityEngine;

public class MouseWorld : MonoBehaviour {
  [SerializeField] private LayerMask mousePlaneMask;
  
  private static MouseWorld instance;
  
  public static Vector3 GetPosition() {
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Physics.Raycast(ray, out var raycastHit, float.MaxValue, instance.mousePlaneMask); 
    return raycastHit.point;  
  }

  private void Awake() {
    if (instance != null) {
      Destroy(gameObject);
      return;
    }
    
    instance = this;
  }

  private void Update(){
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePlaneMask); 
    transform.position = raycastHit.point;
  }
}
