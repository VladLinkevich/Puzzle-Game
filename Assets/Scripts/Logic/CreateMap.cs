using System.Collections.Generic;
using System.Linq;
using Chip;
using Data;
using Factory;
using Point;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class CreateMap : IInitializable
  {
    private readonly IMapFactory _mapFactory;
    
    private MapData _mapData;
    private List<PointFacade> _points;

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
      CreateChips();
    }

    private void CreatePoints()
    {
      _points = new List<PointFacade>(_mapData.SpawnPoints.Length);
      
      for (int i = 0, end = _mapData.SpawnPoints.Length; i < end; ++i)
        _points.Add(_mapFactory.CreatePoint(_mapData.SpawnPoints[i]));
    }

    private void SetNeighbors()
    {
      for (int i = 0, end = _mapData.Neighbors.Length; i < end; ++i)
      {
        PointFacade first = _points[_mapData.Neighbors[i].From];
        PointFacade second = _points[_mapData.Neighbors[i].To];
        
        _mapFactory.CreateTransition(first.transform.position, second.transform.position);
        
        first.SetNeighbors(second);
        second.SetNeighbors(first);
      }
    }

    private void CreateChips()
    {
      for (int i = 0, end = _mapData.ChipSpawnPoints.Length; i < end; ++i)
      {
        int point = _mapData.ChipSpawnPoints[i];

        ChipFacade chip = _mapFactory.CreateChip(_points[point].transform.position);
        _points[point].SetChip(chip);
      }
    }

    private MapData GetMapData() => 
      Resources.Load<MapData>(ResourcePath.MapDataPath);
  }
}