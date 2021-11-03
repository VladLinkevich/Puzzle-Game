using System;
using UnityEngine;

namespace Chip
{
  public class TouchObserver : MonoBehaviour
  {
    public event Action<ChipFacade> Click;
    
    public ChipFacade Chip;
    
    private void OnMouseDown() => 
      Click?.Invoke(Chip);
  }
}