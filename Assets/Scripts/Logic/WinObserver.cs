using System.Collections.Generic;
using Chip;
using Data;
using Point;
using RoundState;
using Unity.VisualScripting;
using UnityEngine;

namespace Logic
{
  public class WinObserver : IBoardDataReader
  {
    private readonly MovePointAnimation _move;
    private readonly ResourceLoader _loader;

    private BoardData _boardData;
    private List<Type> _chipsType;
    private List<PointFacade> _payloadPoint;

    public WinObserver(MovePointAnimation move, ResourceLoader loader)
    {
      _move = move;
      _loader = loader;

      _move.EndAnimation += CheckWin;
    }

    public void Construct(BoardData data)
    {
      _boardData = data;
      
      GetPayloadData();
    }

    private void GetPayloadData()
    {
      int[] chipEndPoints = _loader.LoadMapData().ChipEndPoints;
      GetPayloadChip(chipEndPoints);
      GetPayloadPoint(chipEndPoints);
    }

    private void GetPayloadChip(int[] chipEndPoints)
    {
      Object[] chips = _loader.LoadChips();
      _chipsType = new List<Chip.Type>(chipEndPoints.Length);

      for (int i = 0, end = chipEndPoints.Length; i < end; ++i) 
        _chipsType.Add(chips[chipEndPoints[i]].GetComponent<ChipFacade>().Type);
    }

    private void GetPayloadPoint(int[] chipEndPoints)
    {
      _payloadPoint = new List<PointFacade>(chipEndPoints.Length);

      for (int i = 0, end = chipEndPoints.Length; i < end; ++i) 
        _payloadPoint.Add(_boardData.Points[i]);
    }

    private void CheckWin()
    {
      for (int i = 0, end = _chipsType.Count; i < end; ++i)
        if (CheckChip(i))
          return;

      Debug.Log("win");
    }

    private bool CheckChip(int i) =>
      _payloadPoint[i].GetChip() == null ||
      _payloadPoint[i].GetChip().Type != _chipsType[i];
  }
}