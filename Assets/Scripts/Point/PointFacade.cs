using System.Collections.Generic;
using Chip;
using UnityEngine;

namespace Point
{
  public class PointFacade : MonoBehaviour
  {
    public GameObject ActiveFX;
    
    private List<PointFacade> _neighbors = new List<PointFacade>();
    private ChipFacade _chip;
    private bool _isActive;

    public bool IsActive => _isActive;
    public List<PointFacade> GetNeighbors() => _neighbors;
    public void SetNeighbors(PointFacade neighbor) => _neighbors.Add(neighbor);
    public ChipFacade GetChip() => _chip;
    public void SetChip(ChipFacade chip) => _chip = chip;

    public void Activate()
    {
      _isActive = true;
      ActiveFX.SetActive(true);
    }

    public void Deactivate()
    {
      _isActive = false;
      ActiveFX.SetActive(false);
    }
  }
}