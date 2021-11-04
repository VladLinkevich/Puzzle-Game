using System.Collections.Generic;
using Chip;
using Data;
using Logic;
using Point;

namespace RoundState
{
  public class SelectPoint : IBoardDataReader
  {
    private readonly BoardFactory _boardFactory;
    private readonly MovePointAnimation _movePoint;

    private BoardData _data;
    private List<PointFacade> _activePoint;
    private PointFacade _currentPoint;
    private bool _isSubscribeOnTouch;
    
    public SelectPoint(MovePointAnimation movePoint)
    {
      _movePoint = movePoint;
    }

    public void Construct(BoardData data)
    {
      _data = data;
      _activePoint = new List<PointFacade>(_data.Points.Count);
    }
    
    public void CalculatePath(ChipFacade chip)
    {
      PointFacade point = GetCurrentPoint(chip);
      if (_currentPoint == point) 
        return;

      _currentPoint = point;
      DeactivateAndUnsubscribePoints();
      CheckNeighborPointOnActive(_currentPoint);
      SubscribeOnPointTouch();
    }

    private void CheckNeighborPointOnActive(PointFacade point)
    {
      List<PointFacade> neighbors = point.GetNeighbors();
      
      foreach (PointFacade p in neighbors)
        if (IsMovablePoint(p))
          ActivatePoint(p);
    }

    private void TouchPoint(PointFacade point)
    {
      if (point.IsActive == false)
        return;
      
      _movePoint.MoveChip(_currentPoint, point);
      DeactivateAndUnsubscribePoints();
    }

    private void SubscribeOnPointTouch()
    {
      foreach (PointFacade point in _activePoint)
        point.GetComponentInChildren<Point.TouchObserver>().Click += TouchPoint;
    }

    private void UnsubscribeOnPointTouch()
    {
      foreach (PointFacade point in _activePoint)
        point.GetComponentInChildren<Point.TouchObserver>().Click -= TouchPoint;
    }

    private void DeactivateAndUnsubscribePoints()
    {
      UnsubscribeOnPointTouch();
      foreach (PointFacade point in _activePoint) 
        point.Deactivate();
      
      _activePoint.Clear();
    }

    private void ActivatePoint(PointFacade p)
    {
      p.Activate();
      _activePoint.Add(p);
      CheckNeighborPointOnActive(p);
    }

    private PointFacade GetCurrentPoint(ChipFacade chip) => 
     _data.Points.Find(x => x.GetChip() == chip);

    private static bool IsMovablePoint(PointFacade p) => 
      p.IsActive == false && p.GetChip() == null;
  }
}