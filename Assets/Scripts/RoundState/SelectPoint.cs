using UnityEngine;

namespace TurnStateMachine
{
  public class SelectPoint : IRoundState
  {
    public void Enter()
    {
      Debug.Log("enter Select point");
    }

    public void Exit()
    {
      throw new System.NotImplementedException();
    }
  }
}