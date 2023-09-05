using System.Threading.Tasks;
using UnityEngine;

public class ActionComponent : MonoBehaviour {
  private static readonly int isMoving = Animator.StringToHash("IsMoving");
  private static readonly int attack = Animator.StringToHash("Attack");

  private readonly float moveSpeed = 5f;
  private readonly float rotateSpeed = 10f;
  private readonly float stoppingDistance = 0.1f;
  private Animator unitAnimator;

  private void Awake() {
    unitAnimator = GetComponent<Animator>();
  }

  public async Task Attack(GridPosition gridClickPosition) {
    // Move to target
    var initialPosition = transform.position;
    var targetPosition = LevelGrid.Instance.GetWorldPosition(gridClickPosition);
    var closestPoint = LevelGrid.Instance
      .GetOccupant(gridClickPosition)
      ?.GetComponent<AttackableComponent>()
      ?.GetClosestPoint(initialPosition);

    // if no value return true to finish attack
    if (!closestPoint.HasValue) return;

    unitAnimator.SetBool(isMoving, true);
    // TODO: should be configurable on weapon
    var weaponOffsetDistance = Vector3.Distance(transform.position, closestPoint.Value) / 2;

    while (Vector3.Distance(transform.position, closestPoint.Value) >= weaponOffsetDistance) {
      PerformMove(targetPosition);

      await Task.Yield();
    }

    unitAnimator.SetTrigger(attack);

    // I
    while (!unitAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
      await Task.Yield();
    }

    var animationInMs = unitAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 1000;
    await Task.Delay(Mathf.RoundToInt(animationInMs));

    // Get back
    while (Vector3.Distance(transform.position, initialPosition) >= 0.01f) {
      PerformMove(initialPosition);

      await Task.Yield();
    }

    unitAnimator.SetBool(isMoving, false);
  }


  public async Task Move(GridPosition gridClickPosition) {
    var moveTarget = LevelGrid.Instance.GetWorldPosition(gridClickPosition);
    var oldGridPosition = LevelGrid.Instance.GetPosition(transform.position);
    unitAnimator.SetBool(isMoving, true);

    while (Vector3.Distance(transform.position, moveTarget) >= stoppingDistance) {
      PerformMove(moveTarget);

      await Task.Yield();
    }

    unitAnimator.SetBool(isMoving, false);
    var newGridPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.MoveUnit(oldGridPosition, newGridPosition);
  }

  private void PerformMove(Vector3 targetPosition) {
    var moveDir = (targetPosition - transform.position).normalized;
    var targetRotation = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    transform.position += moveDir * (moveSpeed * Time.deltaTime);
  }
}