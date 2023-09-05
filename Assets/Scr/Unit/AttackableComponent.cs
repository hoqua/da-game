using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class AttackableComponent : MonoBehaviour {
  private static readonly int Damage = Animator.StringToHash("Damage");

  [Required]
  [SerializeField]
  private UnitStatsScriptableObject enemyScriptableObject;

  [Required]
  public DamagePopUp damagePopupPrefab;

  private Animator _animator;
  private Collider _collider;
  private float _currentAttackPower;
  private float _currentHealth;
  private Color _originalColor;
  private SkinnedMeshRenderer _renderer;


  private void Awake() {
    _currentHealth = enemyScriptableObject.maxHealth;
    _currentAttackPower = enemyScriptableObject.attackPower;
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

  public void TakeDamage(int damage) {
    _renderer.material.color = new Color(1, 0, 0, .1f);
    // _animator.SetTrigger(Damage);
    ShowDamageText(damage);
    StartCoroutine(ChangeBackToOriginalMaterial());
  }

  private void ShowDamageText(int damage) {
    var popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
    popup.GetComponent<DamagePopUp>()?.SetText(damage.ToString());
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