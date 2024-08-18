using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : OnGroundState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] ClipTransition _clip;
    [SerializeField] ClipTransition _startClip;
    AnimancerState _startState;
    AnimancerState _walkState;
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);

        if(state.previousState == state.IdlingState)
        {
            _startState = _animancer.Play(_startClip,0.1f);
            _startState.Speed = 2f;
        }
        else
        {
            _walkState = _animancer.Play(_clip, .35f);
            _walkState.Speed = 1f;

        }
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
        if (_startState != null)
        {
            if (_startState.NormalizedTime >= .9f) 
            {
                _startState = null;
                state.SwitchState(state.RunningState);
                return;
            }
        }
        if (Mathf.Abs(InputManager.instance.verticalInput) <= .1f && Mathf.Abs(InputManager.instance.horizontalInput) <= .1f)
        {
            state.SwitchState(state.IdlingState);
            return;
        }
        if (InputManager.instance.isSprinting)
        {
            state.SwitchState(state.SprintingState);
            return;
        }
    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {

    }

}
