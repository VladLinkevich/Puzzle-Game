using System.Collections.Generic;
using Chip;
using Logic;
using Point;
using UnityEngine;

namespace RoundState
{
  public class SelectPoint 
  {
    private readonly CreateMap _map;
    
    private List<PointFacade> _point;
    private List<Point.TouchObserver> _pointTouchObserver;

    private List<PointFacade> _activePoint;
    private PointFacade _currentPoint;
    private bool _isSubscribeOnTouch;
    
    public SelectPoint(CreateMap map)
    {
      _map = map;
      
      _map.Create += OnCreate;
    }

    public void CalculatePath(ChipFacade chip)
    {
      DeactivatePoints();
      SubscribeOnPointTouch();
      GetCurrentPoint(chip);
      CheckNeighborPointOnActive(_currentPoint);
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
      Debug.Log("Touch point");
    }

    private void SubscribeOnPointTouch()
    {
      if (_isSubscribeOnTouch == true)
        return;

      _isSubscribeOnTouch = true;
      foreach (Point.TouchObserver touchObserver in _pointTouchObserver) 
        touchObserver.Click += TouchPoint;
    }

    private void GetPoints()
    { 
      _point = _map.GetPoints();
      _activePoint = new List<PointFacade>(_point.Count);
      _pointTouchObserver = new List<Point.TouchObserver>(_point.Count);

      for (int i = 0, end = _point.Count; i < end; ++i)
        _pointTouchObserver.Add(_point[i].GetComponentInChildren<Point.TouchObserver>());
    }

    private void DeactivatePoints()
    {
      foreach (PointFacade point in _activePoint) 
        point.Deactivate();
    }

    private void ActivatePoint(PointFacade p)
    {
      p.Activate();
      _activePoint.Add(p);
      CheckNeighborPointOnActive(p);
    }

    private void GetCurrentPoint(ChipFacade chip) => 
      _currentPoint = _point.Find(x => x.GetChip() == chip);

    private static bool IsMovablePoint(PointFacade p) => 
      p.IsActive == false && p.GetChip() == null;
  }
}