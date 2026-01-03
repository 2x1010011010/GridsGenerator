using _2DGrids;
using UnityEngine;

namespace UI._2DGridScene
{
  public class UIObserver : MonoBehaviour
  {
    [SerializeField] private GridSpawner _gridSpawner;

    private void GenerateHexGridButtonClicked() => 
      _gridSpawner.SpawnHexGrid();

    private void GenerateRectGridButtonClicked() => 
      _gridSpawner.SpawnRectGrid();
  }
}