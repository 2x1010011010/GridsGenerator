using UnityEngine;

namespace _3DGrids
{
  public class GridSpawner3D : MonoBehaviour
  {
    [SerializeField] private SpawnerConfig3D _spawnerConfig;
    
    private GridGenerator3D _gridGenerator;
    private ICell3D[,,] _grid;
    private VirtualCell3D[,,] _virtualGrid;
    
    public void SpawnFlatRectGrid()
    {
    }

    public void SpawnFlatHexGrid()
    {
    }

    public void Spawn3DRectGrid()
    {
    }
    
    public void Spawn3DHexGrid()
    {
    }
    
  }
}