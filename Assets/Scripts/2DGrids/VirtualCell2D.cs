using UnityEngine;

namespace _2DGrids
{
  public class VirtualCell2D
  {
    private Vector2Int _position;

    public Vector2Int Position => _position;
    
    public VirtualCell2D(Vector2Int position)
    {
      _position = position;
    }

    public VirtualCell2D(int x, int y)
    {
      var position = new Vector2Int(x, y);
      _position = position;
    }
  }
}