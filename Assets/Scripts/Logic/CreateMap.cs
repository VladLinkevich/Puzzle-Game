using System.Collections.Generic;
using Data;
using Factory;
using Points;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class CreateMap : IInitializable
  {
    private readonly IMapFactory _mapFactory;
    
    private MapData _mapData;
    private List<Point> _points;

    public CreateMap(IMapFactory mapFactory)
    {
      _mapFactory = mapFactory;
    }

    public void Initialize() => 
      CreateBoard();

    private void CreateBoard()
    {
      _mapData = GetMapData();
      CreatePoints();
      SetNeighbors();
    }

    private void SetNeighbors()
    {
      for (int i = 0, end = _mapData.Neighbors.Length; i < end; ++i)
      {
        Point first = _points[_mapData.Neighbors[i].From];
        Point second = _points[_mapData.Neighbors[i].To];
        
        _mapFactory.CreateTransition(first.transform.position, second.transform.position);
        
        first.SetNeighbors(second);
        second.SetNeighbors(first);
      }
    }

    private void CreatePoints()
    {
      _points = new List<Point>(_mapData.SpawnPoints.Length);
      
      for (int i = 0, end = _mapData.SpawnPoints.Length; i < end; ++i)
        _points.Add(_mapFactory.CreatePoint(_mapData.SpawnPoints[i]).GetComponent<Point>());
    }

    private MapData GetMapData() => 
      Resources.Load<MapData>(ResourcePath.MapDataPath);
  }
}