using System.Collections.Generic;
using UnityEngine;

namespace _3DGrids
{
  [CreateAssetMenu(fileName = "MapSpawner3DConfig", menuName = "Static Data/3D/Spawner Config", order = 0)]
  public class SpawnerConfig3D : ScriptableObject
  {
    public int Length;
    public int Width;
    public int Height;

    public Vector3 RectCellSize;
    public Vector3 HexCellSize;

    public List<GameObject> RectPrefabs;
    public List<GameObject> HexPrefabs;

    [Header("Noise Settings")] 
    public float NoiseScale;
    public float HeightMultiplier;
    [Range(0, 1)] public float WaterLevel;
    [Range(3, 4)] public float MountainLevel;
    public Vector2 Offset;
  }
}