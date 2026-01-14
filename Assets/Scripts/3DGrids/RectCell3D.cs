using System.Collections.Generic;
using UnityEngine;

namespace _3DGrids
{
  public class RectCell3D : MonoBehaviour, ICell3D
  {
    public List<Vector3Int> Neighbors { get; }
    public void Setup(Vector3Int position)
    {
      throw new System.NotImplementedException();
    }

    public void SetEmpty(bool isEmpty)
    {
      throw new System.NotImplementedException();
    }
  }
}