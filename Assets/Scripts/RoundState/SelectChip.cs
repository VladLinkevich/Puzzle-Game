using System.Collections.Generic;
using Chip;
using Data;
using Logic;
using TurnStateMachine;
using UnityEngine;
using Zenject;

namespace RoundState
{
  public class SelectChip
  {
    private readonly SelectPoint _selectPoint;
    private readonly BoardFactory _boardFactory;
    private readonly MovePointAnimation _animation;

    private BoardData _data;
    private ChipFacade _currentChip;
    private List<TouchObserver> _chips;

    public SelectChip(SelectPoint selectPoint, MovePointAnimation animation)
    {

      _selectPoint = selectPoint;
      _animation = animation;
    }

    public void Construct(BoardData data)
    {
      _data = data;
      _animation.StartAnimation += BlockMove;
      _animation.EndAnimation += ReleaseMove;
      GetChips();
      SubscribeOnChipClick();
    }

    private void SubscribeOnChipClick()
    {
      foreach (Chip.TouchObserver chip in _chips) 
        chip.Click += TouchChip;
    }

    private void UnsubscribeOnChipClick()
    {
      foreach (Chip.TouchObserver chip in _chips) 
        chip.Click -= TouchChip;
    }

    private void BlockMove()
    {
      _currentChip.Deactivate();
      _currentChip = null;
      UnsubscribeOnChipClick();
    }

    private void ReleaseMove() => 
      SubscribeOnChipClick();

    private void TouchChip(ChipFacade chip)
    {
      if (_currentChip != null)
        _currentChip.Deactivate();

      _currentChip = chip;
      _currentChip.Activate();
      _selectPoint.CalculatePath(chip);
    }

    private void GetChips()
    {
      List<ChipFacade> chips = _data.Chips;
      _chips = new List<TouchObserver>(chips.Count);

      for (int i = 0, end = chips.Count; i < end; ++i)
        _chips.Add(chips[i].GetComponentInChildren<Chip.TouchObserver>());
    }
  }
}