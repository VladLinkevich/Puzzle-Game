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
      ChipFacade chip = from.GetChip();
      
      Sequence sequence = CreateAnimation(chip);
      OnEndAnimation(from, to, sequence, chip);
    }

    private Sequence CreateAnimation(ChipFacade chip)
    {
      Sequence sequence = DOTween.Sequence();
      foreach (PointFacade point in _minPath)
        sequence.Append(chip.transform.DOMove(point.transform.position, Duration));
      return sequence;
    }
    
    private void OnEndAnimation(PointFacade from, PointFacade to, Sequence sequence, ChipFacade chip)
    {
      from.SetChip(null);

      sequence.OnComplete(() =>
      {
        to.SetChip(chip);
        EndAnimation?.Invoke();
      });
    }
  }
}