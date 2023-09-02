using System.Collections;
using UnityEngine;

// TODO: required colliider
public class AttackableComponent : MonoBehaviour {
  private static readonly int Damage = Animator.StringToHash("Damage");

  [SerializeField] private EnemyScriptableObject enemyScriptableObject;

  private Animator _animator;
  private Collider _collider;
  private float _currentDamage;

  // Current stats
  private float _currentHealth;
  private Color _originalColor;
  private SkinnedMeshRenderer _renderer;

  private void Awake() {
    _currentHealth = enemyScriptableObject.MaxHealth;
    _currentDamage = enemyScriptableObject.Damage;
    _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    _animator = GetComponent<Animator>();
    _collider = GetComponent<Collider>();
  }

  private void Start() {
    _originalColor = _renderer.material.color;
  }

  public Vector3 GetSize() {
    return _renderer.bounds.size;
  }

  public void TakeDamage() {
    _renderer.material.color = new Color(1, 0, 0, .1f);
    _animator.SetTrigger(Damage);

    StartCoroutine(ChangeBackToOriginalMaterial());
  }

  private IEnumerator ChangeBackToOriginalMaterial() {
    // Wait for a certain time before changing the material back
    yield return new WaitForSeconds(.1f); // Change the delay time as needed

    // Revert to the original material
    _renderer.material.color = _originalColor;
  }

  public Vector3 GetClosestPoint(Vector3 position) {
    return _collider.ClosestPoint(position);
  }
}