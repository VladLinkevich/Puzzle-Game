using System;
using System.Collections.Generic;
using System.Linq;
using Point;
using UnityEngine;

namespace Logic
{
  public class PathFinder
  {
    private int _minDistance;
    private  List<PointFacade> _minPath;
    
    public List<PointFacade> FindMinPath(PointFacade from, PointFacade to)
    {
      _minDistance = Int32.MaxValue;
      FindPathMinDistance(new List<PointFacade>(), from, to, 0);
      _minPath = FindPath(new List<PointFacade>(), from, to, _minDistance);
      return _minPath;
    }

    private List<PointFacade> FindPath(List<PointFacade> path, PointFacade from, PointFacade to, int distance)
    {
      if (from == to)
      {
        path.Add(from);
        path.RemoveAt(0);
        return path;
      }

      if (distance <= 0)
        return null;
      
      List<PointFacade> neighbors = from.GetNeighbors();
      foreach (PointFacade neighbor in neighbors)
        if (neighbor.IsActive &&
            !path.Contains(neighbor))
        {
          path.Add(from);
          if (FindPath(path, neighbor, to, distance - 1) == null)
            path.Remove(from);
          else
            return path;
        }

      return null;
    }

    private void FindPathMinDistance(List<PointFacade> path, PointFacade @from, PointFacade to, int minDistance)
    {
      if (from == to &&
          _minDistance != minDistance)
      {
        _minDistance = minDistance;
        return;
      }
      
      if (_minDistance <= minDistance)
        return;

      List<PointFacade> neighbors = from.GetNeighbors();
      foreach (PointFacade neighbor in neighbors)
        if (neighbor.IsActive &&
            !path.Contains(neighbor))
        {
          path.Add(from);
          FindPathMinDistance(path, neighbor, to, minDistance + 1);
          path.Remove(from);
        }
    }
  }
}