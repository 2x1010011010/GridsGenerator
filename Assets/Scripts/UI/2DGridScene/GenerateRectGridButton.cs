using System;
using UnityEngine;

namespace UI._2DGridScene
{
  public class GenerateRectGridButton : ButtonBase
  {
    public event Action OnGenerateRectGridButtonClicked;
    
    protected override void ButtonClick() => 
      OnGenerateRectGridButtonClicked?.Invoke();
  }
}