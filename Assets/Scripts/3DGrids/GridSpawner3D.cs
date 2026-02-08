using UnityEngine;

namespace _3DGrids
{
  public class GridSpawner3D : MonoBehaviour
  {
    [SerializeField] private SpawnerConfig3D _config;

    private GameObject[,,] _spawned;
    private VirtualCell3D[,,] _virtualGrid;
    private GridGenerator3D _generator;

    #region PUBLIC API

    public void SpawnFlatRectGrid()
    {
      GenerateFlat();
      SpawnRect();
    }

    public void SpawnFlatHexGrid()
    {
      GenerateFlat();
      SpawnHex(false);
    }

    public void Spawn3DRectGrid()
    {
      Generate3D();
      SpawnRect();
    }

    public void Spawn3DHexGrid()
    {
      Generate3D();
      SpawnHex(true);
    }

    #endregion

    #region GENERATION

    private void GenerateFlat()
    {
      ClearGrid();
      _generator = new GridGenerator3D(_config.Length, 1, _config.Width);
      _virtualGrid = _generator.GenerateFlatNoise
      (new NoiseSettings(_config.NoiseScale,
        _config.HeightMultiplier,
        _config.WaterLevel, _config.MountainLevel,
        _config.Offset));
    }

    private void Generate3D()
    {
      ClearGrid();
      _generator = new GridGenerator3D(_config.Length, _config.Height, _config.Width);
      _virtualGrid = _generator.Generate3DNoise
      (new NoiseSettings(_config.NoiseScale,
        _config.HeightMultiplier,
        _config.WaterLevel, _config.MountainLevel,
        _config.Offset));
    }

    #endregion

    #region SPAWN RECT

    private void SpawnRect()
    {
      InitSpawnArray();

      for (int x = 0; x < _virtualGrid.GetLength(0); x++)
      for (int y = 0; y < _virtualGrid.GetLength(1); y++)
      for (int z = 0; z < _virtualGrid.GetLength(2); z++)
      {
        var cell = _virtualGrid[x, y, z];
        if (cell == null) continue;

        Vector3 pos = new Vector3(
          x * _config.RectCellSize.x,
          y * _config.RectCellSize.y,
          z * _config.RectCellSize.z
        );

       // _spawned[x, y, z] = Instantiate(, pos, Quaternion.identity, transform);
      }
    }

    #endregion

    #region SPAWN HEX

    private void SpawnHex(bool is3D)
    {
      InitSpawnArray();

      for (int x = 0; x < _virtualGrid.GetLength(0); x++)
      for (int y = 0; y < _virtualGrid.GetLength(1); y++)
      for (int z = 0; z < _virtualGrid.GetLength(2); z++)
      {
        var cell = _virtualGrid[x, y, z];
        if (cell == null) continue;

        Vector3 pos = GetHexPosition(x, y, z, is3D);

        //_spawned[x, y, z] = Instantiate(, pos, Quaternion.identity, transform);
      }
    }

    private Vector3 GetHexPosition(int x, int y, int z, bool is3D)
    {
      float width = _config.HexCellSize.x;
      float height = _config.HexCellSize.y;
      float depth = _config.HexCellSize.z;

      float offsetX = (z % 2 == 0) ? 0 : width / 2f;

      float worldX = x * width + offsetX;
      float worldZ = z * depth * 0.75f;
      float worldY = is3D ? y * height : 0f;

      return new Vector3(worldX, worldY, worldZ);
    }

    #endregion

    #region HELPERS

    private void InitSpawnArray()
    {
      _spawned = new GameObject[
        _virtualGrid.GetLength(0),
        _virtualGrid.GetLength(1),
        _virtualGrid.GetLength(2)
      ];
    }

    private void ClearGrid()
    {
      if (_spawned == null) return;

      foreach (var obj in _spawned)
        if (obj != null)
          Destroy(obj);

      _spawned = null;
      _virtualGrid = null;
    }

    #endregion
  }
}