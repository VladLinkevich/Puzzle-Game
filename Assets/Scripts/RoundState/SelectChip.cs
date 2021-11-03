using System.Collections.Generic;
using Chip;
using Logic;
using TurnStateMachine;
using UnityEngine;

namespace RoundState
{
  public class SelectChip : IRoundState
  {
    private readonly RoundStateMachine _stateMachine;
    private readonly CreateMap _map;
    private List<Chip.TouchObserver> _chips;

    public SelectChip(RoundStateMachine stateMachine,
      CreateMap map)
    {
      _stateMachine = stateMachine;
      _map = map;
      
      _map.Create += OnCreate;
    }

    public void Enter()
    {
      Debug.Log("Enter Select Chip");
      SubscribeOnChipClick();
    }

    public void Exit() => 
      UnsubscribeOnChipClick();

    private void OnCreate()
    {
      _map.Create -= OnCreate;
      
      List<ChipFacade> chips = _map.GetChips();
      _chips = new List<TouchObserver>(chips.Count);

      for (int i = 0, end = chips.Count; i < end; ++i)
        _chips.Add(chips[i].GetComponentInChildren<Chip.TouchObserver>());

      _stateMachine.ChangeState<SelectChip>();
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

    private void TouchChip(ChipFacade chip)
    {
      _stateMachine.SelectChip = chip;
      _stateMachine.ChangeState<SelectPoint>();
    }
  }
}