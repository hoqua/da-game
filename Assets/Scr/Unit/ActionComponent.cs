using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(StatsComponent))]
public class ActionComponent : MonoBehaviour {
  private static readonly int isMoving = Animator.StringToHash("IsMoving");
  private static readonly int attack = Animator.StringToHash("Attack");

  private readonly float stoppingDistance = 0.1f;
  private StatsComponent _stats;
  private ActionState actionState = ActionState.WaitingInput;
  private Animator unitAnimator;

  private void Awake() {
    unitAnimator = GetComponent<Animator>();
    _stats = GetComponent<StatsComponent>();
  }

  public async Task Attack(GridPosition gridClickPosition) {
    actionState = ActionState.Attacking;
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

    while (!unitAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) await Task.Yield();

    var animationInMs = unitAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 1000;
    await Task.Delay(Mathf.RoundToInt(animationInMs));

    // Get back
    while (Vector3.Distance(transform.position, initialPosition) >= 0.01f) {
      PerformMove(initialPosition);

      await Task.Yield();
    }

    unitAnimator.SetBool(isMoving, false);
    actionState = ActionState.WaitingInput;
  }

  public async Task Move(GridPosition gridClickPosition) {
    actionState = ActionState.Moving;
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
    actionState = ActionState.WaitingInput;
  }

  private void PerformMove(Vector3 targetPosition) {
    var moveDir = (targetPosition - transform.position).normalized;
    var targetRotation = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _stats.GetRotateSpeed() * Time.deltaTime);
    transform.position += moveDir * (_stats.GetMoveSpeed() * Time.deltaTime);
  }

  public ActionState GetState() {
    return actionState;
  }
}


public enum ActionState {
  WaitingInput,
  Moving,
  Attacking
}