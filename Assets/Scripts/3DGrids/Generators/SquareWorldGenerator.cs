using _3DGrids.Configs;
using UnityEngine;

namespace _3DGrids.Generators
{
  public class SquareWorldGenerator : MonoBehaviour
  {
    public SquareWorldConfig config;

    private void Start()
    {
      Random.InitState(config.Noise.Seed);

      for (int z = 0; z < config.WorldChunksZ; z++)
      for (int x = 0; x < config.WorldChunksX; x++)
        CreateChunk(x, z);

      CreateWater();
    }

    void CreateChunk(int cx, int cz)
    {
      GameObject go = new GameObject($"SquareChunk_{cx}_{cz}");
      go.transform.parent = transform;

      var chunk = go.AddComponent<SquareChunk>();
      chunk.Build(cx, cz, config);
    }

    void CreateWater()
    {
      if (config.WaterMaterial == null) return;

      GameObject water = new GameObject("Water");
      water.transform.parent = transform;

      var mf = water.AddComponent<MeshFilter>();
      var mr = water.AddComponent<MeshRenderer>();
      mr.material = config.WaterMaterial;

      Mesh mesh = new Mesh();

      float sizeX = config.WorldChunksX * config.ChunkWorldSize;
      float sizeZ = config.WorldChunksZ * config.ChunkWorldSize;

      mesh.vertices = new Vector3[]
      {
        new(0, config.WaterHeight, 0),
        new(sizeX, config.WaterHeight, 0),
        new(0, config.WaterHeight, sizeZ),
        new(sizeX, config.WaterHeight, sizeZ)
      };

      mesh.triangles = new int[]
      {
        0,2,1,
        2,3,1
      };

      mesh.RecalculateNormals();
      mf.mesh = mesh;
    }
  }
}