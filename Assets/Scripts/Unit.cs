using System;
using UnityEngine;

public class Unit : MonoBehaviour {
  [SerializeField] private Animator unitAnimator;

  private readonly float moveSpeed = 4f;
  private readonly float rotateSpeed = 10f;
  private readonly float stoppingDistance = 0.1f;
  private static readonly int IsWalking = Animator.StringToHash("isWalking");

  private Vector3 targetPosition;
  private GridPosition unitPosition;


  private void Awake() {
    targetPosition = transform.position;
  }

  private void Start() {
    unitPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.SetUnit(this, unitPosition);
  }


  private void Update() {
    // TODO: check MoveTowards
    var distanceToTarget = Vector3.Distance(targetPosition, transform.position);

    if (distanceToTarget > stoppingDistance) {
      var moveDirection = (targetPosition - transform.position).normalized;
      transform.position += moveDirection * (moveSpeed * Time.deltaTime);
      transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

      if (!unitAnimator.GetBool(IsWalking)) {
        unitAnimator.SetBool(IsWalking, true);
      }
    }
    else {
      if (unitAnimator.GetBool(IsWalking)) {
        unitAnimator.SetBool(IsWalking, false);
      }
    }

    var newGridPosition = LevelGrid.Instance.GetPosition(transform.position);

 
    if (!GridPosition.isEquals(newGridPosition, unitPosition)) {
   
      LevelGrid.Instance.UnitMoved(this, unitPosition, newGridPosition);
      unitPosition = newGridPosition;
    }
  }


  public void Move(Vector3 targetPosition) {
    this.targetPosition = targetPosition;
  }
}