using System.Collections;
using UnityEngine;

public class MovementSystem : MonoBehaviour {
  [SerializeField] private Animator unitAnimator;
  [SerializeField] private Transform unitTransform;

  private readonly float moveSpeed = 5f;
  private readonly float rotateSpeed = 10f;
  private readonly float stoppingDistance = 0.1f;
  private static readonly int isMoving = Animator.StringToHash("isMoving");
  private static readonly int attack = Animator.StringToHash("Attack");

  private Vector3? moveTarget;
  private Vector3? attackTraget;
  private Vector3? beforeAttackPosition;
  private bool attackState;

  public void Move(GridPosition targetGridPosition) {
    moveTarget = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
  }

  public void MeleeAttack(GridPosition targetGridPosition) {
    attackTraget = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
  }

  private void Update() {
    TryMove();
    TryAttack();
  }

  private void TryAttack() {
    if (!attackTraget.HasValue || attackState) return;
    StartCoroutine(AttackSequence());
  }

  private IEnumerator AttackSequence() {
    attackState = true;

    // Move to target
    var initialPosition = transform.position;
    var targetPosition = attackTraget.Value;
    targetPosition.y = initialPosition.y; // Keep the character and target at the same height

    unitAnimator.SetBool(isMoving, true);

    while (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
      Move(targetPosition);

      yield return null;
    }

    unitAnimator.SetTrigger(attack);

    Debug.Log(unitAnimator.GetCurrentAnimatorStateInfo(0).length);
    // Wait for attack animation to finish
    yield return new WaitForSeconds(unitAnimator.GetCurrentAnimatorStateInfo(0).length);

    // Get back
    while (Vector3.Distance(transform.position, initialPosition) > 0.1f) {
      Move(initialPosition);

      yield return null;
    }

    unitAnimator.SetBool(isMoving, false);

    // Reset variables
    attackState = false;
    attackTraget = null;
    yield return null;
  }

  private void TryMove() {
    if (!moveTarget.HasValue) return;

    // TODO: check MoveTowards
    var distanceToTarget = Vector3.Distance(moveTarget.Value, unitTransform.position);

    if (distanceToTarget > stoppingDistance) {
      var moveDirection = (moveTarget.Value - unitTransform.position).normalized;
      unitTransform.position += moveDirection * (moveSpeed * Time.deltaTime);
      unitTransform.forward = Vector3.Lerp(unitTransform.forward, moveDirection, Time.deltaTime * rotateSpeed);

      if (!unitAnimator.GetBool(isMoving)) {
        unitAnimator.SetBool(isMoving, true);
      }
    }
    else {
      if (unitAnimator.GetBool(isMoving)) {
        unitAnimator.SetBool(isMoving, false);
        moveTarget = null;
      }
    }
  }

  private void Move(Vector3 targetPosition) {
    Vector3 moveDir = (targetPosition - transform.position).normalized;
    Quaternion targetRotation = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    transform.position += moveDir * (moveSpeed * Time.deltaTime);
  }
}