using UnityEngine;

namespace _3DGrids
{
  public class VirtualCell3D
  {
    private Vector3Int _position;
    public Vector3Int Position => _position;
    public CellType CellType { get; set; }
    
    public VirtualCell3D(Vector3Int position, CellType cellType)
    {
      _position = position;
      CellType = cellType;
    }
    
    public VirtualCell3D(int x, int y, int z, CellType cellType)
    {
      var position = new Vector3Int(x, y, z);
      _position = position;
      CellType = cellType;
    }
  }
}