using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : StateBase
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
        if (characterControl.IsGrounded())
        {
            if(elapsedTime > 1.5f)
            {
                state.SwitchState(state.LandingState);
                return;
            }
            else
            {
                if (Mathf.Abs(InputManager.instance.verticalInput) >= .1f || Mathf.Abs(InputManager.instance.horizontalInput) >= .1f)
                {
                    state.SwitchState(state.RunningState);
                    return;
                }
                else
                {
                    state.SwitchState(state.IdlingState);
                    return;
                }
            }
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
