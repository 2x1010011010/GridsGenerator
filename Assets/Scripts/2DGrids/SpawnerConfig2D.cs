using UnityEngine;

namespace _2DGrids
{
  [CreateAssetMenu(fileName = "SpawnerConfig2D", menuName = "Static Data/2D/Spawner Config", order = 0)]
  public class SpawnerConfig2D : ScriptableObject
  {
    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    
    [field: SerializeField] public GameObject RectCellPrefab { get; private set; }
    [field: SerializeField] public GameObject HexCellPrefab { get; private set; }
    [field: SerializeField] public Vector2 CellSize { get; private set; }

    
    [field: SerializeField] public Color Default { get; private set; } = Color.white;
    [field: SerializeField] public Color Selected { get; private set; } = Color.green;
  }
}