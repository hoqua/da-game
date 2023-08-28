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
  private bool moveState;

  public void Move(GridPosition targetGridPosition) {
    moveTarget = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
  }

  public void MeleeAttack(GridPosition targetGridPosition) {
    attackTraget = LevelGrid.Instance.GetOccupant(targetGridPosition).transform.position;
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
    var unitGridPosition = LevelGrid.Instance.GetPosition(targetPosition);
    var attackable = LevelGrid.Instance.GetOccupant(unitGridPosition).GetComponent<AttackableComponent>();
    
    var maxEnemyDimension = Mathf.Max(attackable.GetSize().x, attackable.GetSize().z); // Consider the maximum dimension

    // Calculate the attack offset based on the enemy size

    unitAnimator.SetBool(isMoving, true);

    while (Vector3.Distance(transform.position , targetPosition) > maxEnemyDimension) {
      Move(targetPosition);

      yield return null;
    }

    unitAnimator.SetTrigger(attack);
    
    // Wait for attack animation to finish
    var halfOfAnimationTime = unitAnimator.GetCurrentAnimatorStateInfo(0).length / 2;
    yield return new WaitForSeconds(halfOfAnimationTime);
    
    attackable.TakeDamage();
    
    yield return new WaitForSeconds(halfOfAnimationTime);
    
    // Get back
    while (Vector3.Distance(transform.position, initialPosition) >= 0.01f) {
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
    if (!moveTarget.HasValue || moveState) return;
    StartCoroutine(Movement());
  }

  private IEnumerator Movement() {
    moveState = true;
    var oldGridPosition =  LevelGrid.Instance.GetPosition(transform.position);
    unitAnimator.SetBool(isMoving, true);
    
    while (Vector3.Distance(transform.position, moveTarget.Value) >= stoppingDistance) {
      Move(moveTarget.Value);
      
      yield return null;
    }
    
    unitAnimator.SetBool(isMoving, false);
    var newGridPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.UnitMoved(this, oldGridPosition, newGridPosition);
    
    // Reset variables
    moveState = false;
    moveTarget = null;
    yield return null;
  }

  private void Move(Vector3 targetPosition) {
    var moveDir = (targetPosition - transform.position).normalized;
    var targetRotation = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    transform.position += moveDir * (moveSpeed * Time.deltaTime);
  }
}