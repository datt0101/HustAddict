using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : StateBase
{
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
        CharacterStop();
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
        CharacterMove();
        if (InputManager.instance.JumpTriggered())
        {   
            state.SwitchState(state.JumpingState);
            return;
        }
        if (characterControl.IsFalling())
        {
            state.SwitchState(state.FallingState);
            return;
        }
    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {
        
    }
    public void CharacterMove()
    {
        characterControl.SetMovementDirection(characterControl.characterMovementDirection);
    }
    public void CharacterStop()
    {
        characterControl.SetMovementDirection(Vector3.zero);
    }
}
