using System;
using UnityEngine;

namespace Point
{
  public class TouchObserver : MonoBehaviour
  {
    public event Action<PointFacade> Click;
    
    public PointFacade Point;
    
    private void OnMouseDown() => 
      Click?.Invoke(Point);
  }
}