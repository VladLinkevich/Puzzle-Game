using System.Collections.Generic;
using UnityEngine;

namespace Points
{
  public class Point : MonoBehaviour
  {
    private List<Point> _neighbors = new List<Point>();

    public void SetNeighbors(Point neighbor) => 
      _neighbors.Add(neighbor);
  }
}