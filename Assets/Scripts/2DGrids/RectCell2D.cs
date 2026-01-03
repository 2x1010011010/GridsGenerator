using System.Collections.Generic;
using UnityEngine;

namespace _2DGrids
{
  public class RectCell2D : MonoBehaviour, ICell2D
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;
    
    private Vector2Int _position;
    private bool _isEmpty;
    
    private readonly List<Vector2Int> _neighbors = new List<Vector2Int>() 
    { 
      new Vector2Int(0, 1),
      new Vector2Int(1, 0),
      new Vector2Int(0, -1),
      new Vector2Int(-1, 0)
    };
    
    public Vector2Int Position => _position;
    public List<Vector2Int> Neighbors => _neighbors;
    
    public void Setup(Vector2Int position) => 
      _position = position;

    public void SetEmpty(bool isEmpty) => 
      _isEmpty = isEmpty;
  }
}