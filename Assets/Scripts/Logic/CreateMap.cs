using System;
using System.Collections.Generic;
using Chip;
using Data;
using Factory;
using Point;
using RoundState;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class CreateMap : IInitializable
  {
    private readonly IMapFactory _mapFactory;
    private readonly RoundStateMachine _stateMachine;

    public event Action Create;
    
    private MapData _mapData;
    private List<PointFacade> _points;
    private List<ChipFacade> _chips;

    public CreateMap(IMapFactory mapFactory) => 
      _mapFactory = mapFactory;

    public void Initialize() => 
      CreateBoard();

    public List<PointFacade> GetPoints() => 
      _points;

    public List<ChipFacade> GetChips() =>
      _chips;

    private void CreateBoard()
    {
      _mapData = GetMapData();
      CreatePoints();
      SetNeighbors();
      CreateChips();
      Create?.Invoke();
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
      _chips = new List<ChipFacade>(_mapData.ChipSpawnPoints.Length);
      
      for (int i = 0, end = _mapData.ChipSpawnPoints.Length; i < end; ++i)
      {
        int point = _mapData.ChipSpawnPoints[i];
        ChipFacade chip = _mapFactory.CreateChip(_points[point].transform.position);
        _chips.Add(chip);
        _points[point].SetChip(chip);
      }
    }

    private MapData GetMapData() => 
      Resources.Load<MapData>(ResourcePath.MapDataPath);
  }
}