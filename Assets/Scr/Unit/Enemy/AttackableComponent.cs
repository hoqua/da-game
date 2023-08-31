using System.Collections;
using UnityEngine;

public class AttackableComponent : MonoBehaviour {
  [SerializeField]
  private EnemyScriptableObject enemyScriptableObject;
  private SkinnedMeshRenderer _renderer;
  private Animator _animator;
  private Color _originalColor;
  private static readonly int Damage = Animator.StringToHash("Damage");
  
  // Current stats
  private float _currentHealth;
  private float _currentDamage;

  private void Awake() {
    _currentHealth = enemyScriptableObject.MaxHealth;
    _currentDamage = enemyScriptableObject.Damage;
    _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    _animator = GetComponent<Animator>();
  }

  private void Start() {
    _originalColor = _renderer.material.color;
  }

  public Vector3 GetSize() {
    return _renderer.bounds.size;
  }
  
  public void TakeDamage() {
    _renderer.material.color = new Color(1, 0,0, .1f);
    _animator.SetTrigger(Damage);
    
    StartCoroutine(ChangeBackToOriginalMaterial());
  }
  
  private IEnumerator ChangeBackToOriginalMaterial()
  {
    // Wait for a certain time before changing the material back
    yield return new WaitForSeconds(.1f); // Change the delay time as needed

    // Revert to the original material
    _renderer.material.color = _originalColor;
  }
}