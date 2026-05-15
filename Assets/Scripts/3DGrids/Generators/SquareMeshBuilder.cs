using _3DGrids.Configs;
using UnityEngine;

namespace _3DGrids.Generators
{
  public static class SquareMeshBuilder
  {
    public static Mesh BuildChunk(int chunkX, int chunkZ, SquareWorldConfig config)
    {
      Mesh mesh = new Mesh();
      mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

      int size = config.TilesPerChunk;
      float tile = config.TileSize;

      Vector3[] vertices = new Vector3[(size + 1) * (size + 1)];
      int[] triangles = new int[size * size * 6];
      Color[] colors = new Color[vertices.Length];

      for (int z = 0; z <= size; z++)
      for (int x = 0; x <= size; x++)
      {
        float worldX = x * tile + chunkX * config.ChunkWorldSize;
        float worldZ = z * tile + chunkZ * config.ChunkWorldSize;

        float height = Mathf.PerlinNoise(
          worldX * config.Noise.NoiseScale,
          worldZ * config.Noise.NoiseScale
        ) * config.Noise.MaxHeight;

        int index = z * (size + 1) + x;
        vertices[index] = new Vector3(x * tile, height, z * tile);
        colors[index] = GetColor(height);
      }

      int triIndex = 0;
      for (int z = 0; z < size; z++)
      for (int x = 0; x < size; x++)
      {
        int i = z * (size + 1) + x;

        triangles[triIndex++] = i;
        triangles[triIndex++] = i + size + 1;
        triangles[triIndex++] = i + 1;

        triangles[triIndex++] = i + 1;
        triangles[triIndex++] = i + size + 1;
        triangles[triIndex++] = i + size + 2;
      }

      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.colors = colors;
      mesh.RecalculateNormals();

      return mesh;
    }

    static Color GetColor(float h)
    {
      if (h < 2f) return new Color(0.9f, 0.8f, 0.6f);
      if (h < 6f) return Color.green;
      if (h < 8f) return Color.gray;
      return Color.white;
    }
  }
}