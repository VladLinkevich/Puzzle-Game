using System;
using System.Collections.Generic;
using Chip;
using DG.Tweening;
using Logic;
using Point;

namespace RoundState
{
  public class MovePointAnimation
  {
    private const float Duration = 0.5f;
    public event Action StartAnimation;
    public event Action EndAnimation;
    
    private readonly PathFinder _pathFinder;

    private List<PointFacade> _minPath;

    public MovePointAnimation(PathFinder pathFinder)
    {
      _pathFinder = pathFinder;
    }

    public void MoveChip(PointFacade from, PointFacade to)
    {
      StartAnimation?.Invoke();
      _minPath = _pathFinder.FindMinPath(from, to);

      Sequence sequence = DOTween.Sequence();
      ChipFacade chip = from.GetChip();
      foreach (PointFacade point in _minPath)
        sequence.Append(chip.transform.DOMove(point.transform.position, Duration));
      
      ChangeChipHandler(from, to);
      sequence.OnComplete(() => EndAnimation?.Invoke());
    }

    private void ChangeChipHandler(PointFacade from, PointFacade to)
    {
      to.SetChip(from.GetChip());
      from.SetChip(null);
    }
  }
}