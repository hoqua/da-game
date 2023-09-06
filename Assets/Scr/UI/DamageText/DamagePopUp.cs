using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour {
  public AnimationCurve opacityCurve;
  public AnimationCurve scaleCurve;
  public AnimationCurve heightCurve;
  private Vector3 origin;
  private float time;

  private TextMeshProUGUI tmp;

  private void Awake() {
    tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    origin = transform.position;
  }

  private async void Start() {
    await DestroyAfterDelay(1f);
  }

  private void Update() {
    tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
    transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
    transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
    time += Time.deltaTime;
  }

  public void SetText(string text) {
    tmp.text = text;
  }

  private async Task DestroyAfterDelay(float delay) {
    await Task.Delay((int)(delay * 1000));
    Destroy(gameObject);
  }
}