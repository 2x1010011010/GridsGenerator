using UnityEngine;

namespace _3DGrids
{
  public class NoiseSettings
  {
    public float NoiseScale;
    public float HeightMultiplier;
    public float WaterLevel;
    public float MountainLevel;
    public Vector2 Offset;
    
    public NoiseSettings(float noiseScale, float heightMultiplier, float waterLevel, float mountainLevel, Vector2 offset)
    {
      NoiseScale = noiseScale;
      HeightMultiplier = heightMultiplier;
      WaterLevel = waterLevel;
      MountainLevel = mountainLevel;
      Offset = offset;
    }
  }
}