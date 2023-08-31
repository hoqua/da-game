using System.Threading.Tasks;
using UnityEngine;
using Task = System.Threading.Tasks.Task;


public class MovementSystem: MonoBehaviour  {
  [SerializeField] private Animator unitAnimator;

  private readonly float moveSpeed = 5f;
  private readonly float rotateSpeed = 10f;
  private readonly float stoppingDistance = 0.1f;
  private static readonly int isMoving = Animator.StringToHash("isMoving");
  private static readonly int attack = Animator.StringToHash("Attack");
  
  private void Awake() {
    unitAnimator = GetComponent<Animator>();
  }

  public async Task<bool> Attack(GridPosition gridClickPosition) {
    // Move to target
    var initialPosition = transform.position;
    var targetPosition = LevelGrid.Instance.GetWorldPosition(gridClickPosition);
    var attackable = LevelGrid.Instance.GetOccupant(gridClickPosition).GetComponent<AttackableComponent>();
    
    var maxEnemyDimension = Mathf.Max(attackable.GetSize().x, attackable.GetSize().z); // Consider the maximum dimension
    
    unitAnimator.SetBool(isMoving, true);

    while (Vector3.Distance(transform.position , targetPosition) > maxEnemyDimension) {
      PerformMove(targetPosition);

      await Task.Yield();
    }

    unitAnimator.SetTrigger(attack);
    
    // Wait for attack animation to finish
    var animationInMs = unitAnimator.GetCurrentAnimatorStateInfo(0).length * 1000;
    await Task.Delay(Mathf.RoundToInt(animationInMs));
    
    // Get back
    while (Vector3.Distance(transform.position, initialPosition) >= 0.01f) {
      PerformMove(initialPosition);

      await Task.Yield();
    }

    unitAnimator.SetBool(isMoving, false);
    return true;
  }
  

  public async Task<bool> Move(GridPosition gridClickPosition) {
    var moveTarget = LevelGrid.Instance.GetWorldPosition(gridClickPosition);
    var oldGridPosition =  LevelGrid.Instance.GetPosition(transform.position);
    unitAnimator.SetBool(isMoving, true);
    
    while (Vector3.Distance(transform.position, moveTarget) >= stoppingDistance) {
      PerformMove(moveTarget);
      
      await Task.Yield();
    }
    
    unitAnimator.SetBool(isMoving, false);
    var newGridPosition = LevelGrid.Instance.GetPosition(transform.position);
    LevelGrid.Instance.MoveUnit( oldGridPosition, newGridPosition);
    
    return true;
  }

  private void PerformMove(Vector3 targetPosition) {
    var moveDir = (targetPosition - transform.position).normalized;
    var targetRotation = Quaternion.LookRotation(moveDir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    transform.position += moveDir * (moveSpeed * Time.deltaTime);
  }
}