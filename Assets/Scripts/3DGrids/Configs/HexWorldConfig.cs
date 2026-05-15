using UnityEngine;

namespace _3DGrids.Configs
{
  [CreateAssetMenu(menuName = "World/Hex World Config")]
  public class HexWorldConfig : ScriptableObject
  {
    public int WorldChunksX = 4;
    public int WorldChunksZ = 4;

    public int HexPerChunkX = 20;
    public int HexPerChunkZ = 20;

    public float HexSize = 1f;

    public float WaterHeight = 2.5f;
    public Material WaterMaterial;
    public Material TerrainMaterial;

    public NoiseConfig Noise;

    public float ChunkWorldWidth =>
      HexPerChunkX * HexSize * 1.5f;

    public float ChunkWorldHeight =>
      HexPerChunkZ * HexSize * Mathf.Sqrt(3f);
  }
}