namespace _2DGrids
{
  public class GridGenerator2D
  {
    private readonly int _width;
    private readonly int _height;
    private VirtualCell2D[,] _grid;
    
    public GridGenerator2D(int width, int height)
    {
      _width = width;
      _height = height;
      _grid = new VirtualCell2D[_width, _height];
    }
    
    public VirtualCell2D[,] Generate()
    {
      for(int x = 0; x < _width; x++)
        for(int y = 0; y < _height; y++)
          _grid[x, y] = new VirtualCell2D(x, y);
      
      return _grid;
    }
  }
}