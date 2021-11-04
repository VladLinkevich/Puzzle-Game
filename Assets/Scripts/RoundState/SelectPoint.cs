using System.Collections.Generic;
using Chip;
using Logic;
using Point;

namespace RoundState
{
  public class SelectPoint 
  {
    private readonly CreateMap _map;
    private readonly MovePointAnimation _movePoint;

    private List<PointFacade> _point;

    private List<PointFacade> _activePoint;
    private PointFacade _currentPoint;
    private bool _isSubscribeOnTouch;
    
    public SelectPoint(CreateMap map, MovePointAnimation movePoint)
    {
      _map = map;
      _movePoint = movePoint;

      _map.Create += OnCreate;
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

    private void OnCreate()
    {
      _map.Create -= OnCreate;

      GetPoints();
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

    private void GetPoints()
    { 
      _point = _map.GetPoints();
      _activePoint = new List<PointFacade>(_point.Count);
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
      _point.Find(x => x.GetChip() == chip);

    private static bool IsMovablePoint(PointFacade p) => 
      p.IsActive == false && p.GetChip() == null;
  }
}