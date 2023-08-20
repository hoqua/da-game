using System;
using UnityEngine;

public class Unit : MonoBehaviour {
  [SerializeField] private Animator unitAnimator ;
  private Vector3 targetPosition;
  private float moveSpeed = 4f;
  private float rotateSpeed = 10f;
  private float stoppingDistance = 0.1f;
  private static readonly int IsWalking = Animator.StringToHash("isWalking");

  private void Move(Vector3 targetPosition) {
    this.targetPosition = targetPosition;
  }

  private void Update() {
    // TODO: check MoveTowards
    var distanceToTarget = Vector3.Distance(targetPosition, transform.position);
    
    if(distanceToTarget > stoppingDistance) {
      var moveDirection = (targetPosition - transform.position).normalized;
      transform.position += moveDirection * (moveSpeed * Time.deltaTime);
      transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

      if (!unitAnimator.GetBool(IsWalking)) {
        unitAnimator.SetBool(IsWalking, true);
      }
      
    }else{
      if (unitAnimator.GetBool(IsWalking)) {
        unitAnimator.SetBool(IsWalking, false);
      }
      
      
    }

    if (Input.GetMouseButtonDown(0) ) {
      Move(MouseWorld.GetPosition());
    };
    
  }
}