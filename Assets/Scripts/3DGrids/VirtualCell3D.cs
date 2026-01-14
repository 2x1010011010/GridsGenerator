using UnityEngine;

namespace _3DGrids
{
  public class VirtualCell3D
  {
    private Vector3Int _position;
    public Vector3Int Position => _position;
    
    public VirtualCell3D(Vector3Int position)
    {
      _position = position;
    }
    
    public VirtualCell3D(int x, int y, int z)
    {
      var position = new Vector3Int(x, y, z);
      _position = position;
    }
  }
}