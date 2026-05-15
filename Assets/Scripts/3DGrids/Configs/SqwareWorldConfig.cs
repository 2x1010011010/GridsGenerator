using UnityEngine;

namespace _3DGrids.Configs
{
  [CreateAssetMenu(menuName = "World/Square World Config")]
  public class SquareWorldConfig : ScriptableObject
  {
    [Header("World Size")]
    public int WorldChunksX = 4;
    public int WorldChunksZ = 4;

    [Header("Chunk")]
    public int TilesPerChunk = 20;
    public float TileSize = 1f;

    [Header("Water")]
    public float WaterHeight = 2.5f;
    public Material WaterMaterial;

    [Header("Rendering")]
    public Material GrassMaterial;
    public Material DirtMaterial;
    public Material RockMaterial;

    [Header("Vegetation")]
    public GameObject[] Trees;
    public GameObject[] Bushes;
    public GameObject[] Grass;

    public NoiseConfig Noise;

    public float ChunkWorldSize => TilesPerChunk * TileSize;
  }
}