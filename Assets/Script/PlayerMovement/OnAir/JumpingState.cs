using Animancer;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JumpingState : OnAirState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] AnimationClip _clip;

    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
        characterControl.Jump();
        _animancer.Play(_clip, .25f);
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
        characterControl.StopJumping();
    }
    public override void UpdateState(StateManager state)
    {
        //base.UpdateState(state);
        if(characterControl.IsFalling())
        {
            state.SwitchState(state.FallingState);
            return;
        }
    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {

    }
}

