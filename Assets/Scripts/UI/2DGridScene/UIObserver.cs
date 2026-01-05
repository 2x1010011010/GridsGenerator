using _2DGrids;
using UnityEngine;

namespace UI._2DGridScene
{
  public class UIObserver : MonoBehaviour
  {
    [SerializeField] private GridSpawner _gridSpawner;
    [SerializeField] private GenerateHexGridButton _generateHexGridButton;
    [SerializeField] private GenerateRectGridButton _generateRectGridButton;
    
    private void OnEnable()
    {
      _generateHexGridButton.OnGenerateHexGridButtonClicked += GenerateHexGridButtonClicked;
      _generateRectGridButton.OnGenerateRectGridButtonClicked += GenerateRectGridButtonClicked;
    }

    private void GenerateHexGridButtonClicked() => 
      _gridSpawner.SpawnHexGrid();

    private void GenerateRectGridButtonClicked() => 
      _gridSpawner.SpawnRectGrid();

    private void OnDisable()
    {
      _generateHexGridButton.OnGenerateHexGridButtonClicked -= GenerateHexGridButtonClicked;
      _generateRectGridButton.OnGenerateRectGridButtonClicked -= GenerateRectGridButtonClicked;
    }
  }
}