using System;
using System.Collections.Generic;
using Point;

namespace Logic
{
  public class PathFinder
  {
    private List<PointFacade> _minPath;
    private int _minDistance;
    
    public List<PointFacade> FindMinPath(PointFacade from, PointFacade to)
    {
      _minDistance = Int32.MaxValue;
      FindPathMinDistance(from, to, 0);
      FindPath(new List<PointFacade>(_minDistance), from, to, _minDistance);
      return _minPath;
    }

    private void FindPath(List<PointFacade> path, PointFacade from, PointFacade to, int distance)
    {
      path.Add(from);
      
      if (from == to)
      {
        path.RemoveAt(0);
        _minPath = path;
        return;
      }
      
      if (distance <= 0)
        return;
      
      List<PointFacade> neighbors = from.GetNeighbors();
      foreach (PointFacade neighbor in neighbors)
        if (neighbor.IsActive)
          FindPath(new List<PointFacade>(path), neighbor, to, distance - 1);
    }

    private void FindPathMinDistance(PointFacade from, PointFacade to, int minDistance)
    {
      if (_minDistance <= minDistance)
        return;
      
      if (from == to)
      {
        _minDistance = minDistance;
        return;
      }

      List<PointFacade> neighbors = from.GetNeighbors();
      foreach (PointFacade neighbor in neighbors)
        if (neighbor.IsActive)
          FindPathMinDistance(neighbor, to, minDistance + 1);
    }
  }
}