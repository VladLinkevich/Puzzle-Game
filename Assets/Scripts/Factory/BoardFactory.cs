using System.Collections.Generic;
using Chip;
using Data;
using Factory;
using Point;
using UnityEngine;

namespace Logic
{
  public class BoardFactory
  {
    private readonly IBoardObjectFactory _boardObjectFactory;

    public BoardFactory(IBoardObjectFactory boardObjectFactory) => 
      _boardObjectFactory = boardObjectFactory;

    public BoardData CreateBoard(CreateBoardData createBoardData)
    {
      GameObject parent = _boardObjectFactory.CreateParent();
      List<PointFacade> points = CreatePoints(createBoardData.SpawnPoints);
      List<ChipFacade> chips = CreateChips(createBoardData.ChipSpawnPoints, points);
      SetNeighbors(createBoardData.Neighbors, points);

      return new BoardData
      {
        Parent = parent,
        Points = points,
        Chips = chips
      };
    }

    private List<PointFacade> CreatePoints(Vector2[] spawnPoints)
    {
      List<PointFacade> points = new List<PointFacade>(spawnPoints.Length);

      for (int i = 0, end = spawnPoints.Length; i < end; ++i)
        points.Add(_boardObjectFactory.CreatePoint(spawnPoints[i]));

      return points;
    }

    private List<ChipFacade> CreateChips(int[] chipSpawnPoints, List<PointFacade> points)
    {
      int chipId = 0;
      List<ChipFacade> chips = new List<ChipFacade>(chipSpawnPoints.Length);
      
      for (int i = 0, end = chipSpawnPoints.Length; i < end; ++i)
      {
        int point = chipSpawnPoints[i];
        ChipFacade chip = _boardObjectFactory.CreateChip(points[point].transform.position, chipId++);
        points[point].SetChip(chip);
        chips.Add(chip);
      }
      
      return chips;
    }

    private void SetNeighbors(Neighbor[] neighbors, List<PointFacade> points)
    {
      for (int i = 0, end = neighbors.Length; i < end; ++i)
      {
        PointFacade first = points[neighbors[i].From];
        PointFacade second = points[neighbors[i].To];
        
        _boardObjectFactory.CreateTransition(first.transform.position, second.transform.position);
        
        first.SetNeighbors(second);
        second.SetNeighbors(first);
      }
    }
  }
}