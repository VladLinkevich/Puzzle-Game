using System.Collections.Generic;
using Chip;
using Point;
using TurnStateMachine;
using Zenject;

namespace RoundState
{
  public class RoundStateMachine
  {
    public ChipFacade SelectChip;
    public PointFacade SelectPoint;
    
    private Dictionary<System.Type, IRoundState> _states;
    private System.Type _currentState;

    [Inject]
    private void Construct(SelectChip chip, SelectPoint point)
    {
      _states = new Dictionary<System.Type, IRoundState>
      {

      };
    }

    public void ChangeState<TState>() where TState : class, IRoundState
    {
      if (_currentState == typeof(TState))
        return;
      
      if (_currentState != null) 
        _states[_currentState].Exit();
      _currentState = typeof(TState);
      _states[_currentState].Enter();
    }
  }
}