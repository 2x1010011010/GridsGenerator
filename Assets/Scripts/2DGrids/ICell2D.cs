using System.Collections.Generic;
using UnityEngine;

namespace _2DGrids
{
  public interface ICell2D
  {
    public List<Vector2Int> Neighbors { get;}
    public void Setup(Vector2Int position);
    public void SetEmpty(bool isEmpty);
  }
}