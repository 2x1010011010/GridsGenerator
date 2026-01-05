using System;
using UnityEngine;

namespace UI._2DGridScene
{
  public class GenerateHexGridButton : ButtonBase
  {
    public event Action OnGenerateHexGridButtonClicked;
    
    protected override void ButtonClick() => 
      OnGenerateHexGridButtonClicked?.Invoke();
  }
}