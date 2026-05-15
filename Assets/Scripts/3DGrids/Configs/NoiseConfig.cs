using UnityEngine;

namespace _3DGrids.Configs
{
  [CreateAssetMenu(menuName = "World/Noise Config")]
  public class NoiseConfig : ScriptableObject
  {
    public float NoiseScale = 0.03f;
    public float MaxHeight = 10f;
    public int Seed = 12345;
  }
}