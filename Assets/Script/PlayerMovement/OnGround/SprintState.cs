using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : OnGroundState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] ClipTransition _clip;
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
        characterControl.Sprint();
        characterControl.brakingFriction = .1f;
        _animancer.Play(_clip, .25f);
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
        characterControl.brakingFriction = 1f;
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
        if (Mathf.Abs(InputManager.instance.verticalInput) <= .1f && Mathf.Abs(InputManager.instance.horizontalInput) <= .1f)
        {
            state.SwitchState(state.IdlingState);
            return;
        }
        if (!InputManager.instance.isSprinting)
        {
            characterControl.StopSprinting();
            state.SwitchState(state.RunningState);
            return;
        }
    }


}
