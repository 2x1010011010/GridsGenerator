using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public abstract class ButtonBase : MonoBehaviour
  {
    [SerializeField] protected Button _button;
  
    public Button Button => _button;
    
    private void OnEnable()
    {
      _button.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
      _button.onClick.RemoveListener(ButtonClick);
    }

    public void SetActive(bool active)
    {
      _button.enabled = active;
    }

    protected abstract void ButtonClick();
  }
}