using System.Collections.Generic;
using Chip;
using Logic;
using TurnStateMachine;
using UnityEngine;
using Zenject;

namespace RoundState
{
  public class SelectChip
  {
    private readonly SelectPoint _selectPoint;
    private readonly CreateMap _map;
    private readonly MovePointAnimation _animation;

    private List<Chip.TouchObserver> _chips;

    private ChipFacade _currentChip;

    public SelectChip(SelectPoint selectPoint, CreateMap map, MovePointAnimation animation)
    {
      _selectPoint = selectPoint;
      _map = map;
      _animation = animation;

      _map.Create += OnCreate;
      _animation.StartAnimation += BlockMove;
      _animation.EndAnimation += ReleaseMove;
    }

    private void OnCreate()
    {
      _map.Create -= OnCreate;
      
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
      List<ChipFacade> chips = _map.GetChips();
      _chips = new List<TouchObserver>(chips.Count);

      for (int i = 0, end = chips.Count; i < end; ++i)
        _chips.Add(chips[i].GetComponentInChildren<Chip.TouchObserver>());
    }
  }
}