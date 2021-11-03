using System.Collections.Generic;
using Chip;
using UnityEngine;

namespace Point
{
  public class PointFacade : MonoBehaviour
  {
    private List<PointFacade> _neighbors = new List<PointFacade>();
    private ChipFacade _chip;
    
    public void SetNeighbors(PointFacade neighbor) => 
      _neighbors.Add(neighbor);

    public void SetChip(ChipFacade chip) => 
      _chip = chip;
  }
}