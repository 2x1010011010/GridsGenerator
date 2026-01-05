using UnityEngine;

namespace _2DGrids
{
  public class GridSpawner : MonoBehaviour
  {
    [SerializeField] private SpawnerConfig2D _spawnerConfig;
    
    private RectCell2D[,] _rectGrid;
    private HexCell2D[,] _hexGrid;
    private VirtualCell2D[,] _virtualGrid;
    private GridGenerator2D _gridGenerator;
    
    public void SpawnHexGrid()
    {
      ClearGrid();
      GenerateGrid();
      _hexGrid = new HexCell2D[_spawnerConfig.Width, _spawnerConfig.Height];
      
      for(int y = 0; y < _spawnerConfig.Height; y++)
        for (int x = 0; x < _spawnerConfig.Width; x++)
        {
          var cellObject = Instantiate(_spawnerConfig.HexCellPrefab, GetHexCoordinates(x, y), Quaternion.identity);
          cellObject.GetComponent<SpriteRenderer>().color = _spawnerConfig.Default;
          _hexGrid[x, y] = cellObject.GetComponent<HexCell2D>();
        }
    }
    
    public void SpawnRectGrid()
    {
      ClearGrid();
      GenerateGrid();
      _rectGrid = new RectCell2D[_spawnerConfig.Width, _spawnerConfig.Height];
      
      for(int y = 0; y < _spawnerConfig.Height; y++)
      for (int x = 0; x < _spawnerConfig.Width; x++)
      {
        var cellObject = Instantiate(_spawnerConfig.RectCellPrefab, GetRectCoordinates(x, y), Quaternion.identity);
        cellObject.GetComponent<SpriteRenderer>().color = _spawnerConfig.Default;
        _rectGrid[x, y] = cellObject.GetComponent<RectCell2D>();
      }
    }

    private Vector2 GetRectCoordinates(int x, int y) =>
      new Vector2(x * _spawnerConfig.CellSize.x, y * _spawnerConfig.CellSize.y);

    private Vector2 GetHexCoordinates(int x, int y)
    {
      var coordinateX = _spawnerConfig.CellSize.x;
      var coordinateY = _spawnerConfig.CellSize.y;

      return y % 2 == 0 ? 
        new Vector2(x * coordinateX, y * coordinateY * 3 / 4) : 
        new Vector2(_hexGrid[x, y - 1].transform.position.x + coordinateX / 2,
          _hexGrid[x, y - 1].transform.position.y + coordinateY * 3 / 4);
    }


    private void GenerateGrid()
    {
      _gridGenerator = new GridGenerator2D(_spawnerConfig.Width, _spawnerConfig.Height);
      _virtualGrid = _gridGenerator.Generate();
    }

    private void ClearGrid()
    {
      if (_rectGrid != null)
      {
        foreach (var cell in _rectGrid)
          Destroy(cell.gameObject);
        
        _rectGrid = null;
      }

      if (_hexGrid != null)
      {
        foreach (var cell in _hexGrid)
          Destroy(cell.gameObject);
        
        _hexGrid = null;
      }
      
      _virtualGrid = null;
    }
  }
}