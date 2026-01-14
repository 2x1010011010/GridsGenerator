using System.Collections.Generic;
using UnityEngine;

namespace _3DGrids
{
  public interface ICell3D
  {
    public List<Vector3Int> Neighbors { get;}
    public void Setup(Vector3Int position);
    public void SetEmpty(bool isEmpty);
  }
}