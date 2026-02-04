namespace _3DGrids
{
  public class GridGenerator3D
  {
    private readonly int _lenght;
    private readonly int _width;
    private readonly int _height;
    private VirtualCell3D[,,] _grid;
    
    public GridGenerator3D(int lenght, int height, int width)
    {
      _lenght = lenght;
      _width = width;
      _height = height;
    }

    public VirtualCell3D[,,] GenerateFlatMap()
    {
      _grid = new VirtualCell3D[_lenght, 1, _width];
      
      for (var x = 0; x < _lenght; x++)
        for (var z = 0; z < _width; z++)
          _grid[x, 0, z] = new VirtualCell3D(x, 0, z);
      
      return _grid;
    }

    public VirtualCell3D[,,] Generate3DMap()
    {
      _grid = new VirtualCell3D[_lenght, _height, _width];
      
      for (var x = 0; x < _lenght; x++)
        for (var y = 0; y < _height; y++)  
          for (var z = 0; z < _width; z++)
            _grid[x, y, z] = new VirtualCell3D(x, y, z);
      
      return _grid;
    }
  }
}