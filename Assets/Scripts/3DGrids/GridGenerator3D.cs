using UnityEngine;

namespace _3DGrids
{
  public class GridGenerator3D
  {
    private int _length;
    private int _height;
    private int _width;

    public GridGenerator3D(int length, int height, int width)
    {
      _length = length;
      _height = height;
      _width = width;
    }

    public VirtualCell3D[,,] GenerateFlatNoise(NoiseSettings s)
    {
      var grid = new VirtualCell3D[_length, 1, _width];

      for (int x = 0; x < _length; x++)
      for (int z = 0; z < _width; z++)
      {
        float noise = SampleNoise(x, z, s);

        CellType type =
          noise < s.WaterLevel    ? CellType.Water :
          noise < s.MountainLevel ? CellType.Grass :
                                    CellType.Rock;

        grid[x, 0, z] = new VirtualCell3D(new Vector3Int(x, 0, z), type);
      }

      return grid;
    }

    public VirtualCell3D[,,] Generate3DNoise(NoiseSettings s)
    {
      var grid = new VirtualCell3D[_length, _height, _width];

      for (int x = 0; x < _length; x++)
      for (int z = 0; z < _width; z++)
      {
        float noise = SampleNoise(x, z, s);
        int columnHeight = Mathf.Clamp(
          Mathf.FloorToInt(noise * s.HeightMultiplier),
          0, _height - 1);

        for (int y = 0; y <= columnHeight; y++)
        {
          var type = ResolveType(y, columnHeight, noise, s);
          grid[x, y, z] = new VirtualCell3D(new Vector3Int(x, y, z), type);
        }
      }

      return grid;
    }

    private float SampleNoise(int x, int z, NoiseSettings s)
    {
      return Mathf.PerlinNoise(
        (x + s.Offset.x) / s.NoiseScale,
        (z + s.Offset.y) / s.NoiseScale
      );
    }

    private CellType ResolveType(int y, int maxY, float noise, NoiseSettings s)
    {
      if (y == maxY)
      {
        if (noise < s.WaterLevel)    return CellType.Water;
        if (noise < s.MountainLevel) return CellType.Grass;
        return CellType.Rock;
      }

      return CellType.Sand;
    }
  }
}
