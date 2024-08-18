using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : OnGroundState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] AnimationClip _clip;
    AnimancerState _landState;
    
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
        _landState = _animancer.Play(_clip, .15f);
        characterControl.moveAble = false;
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
        if (Mathf.Abs(InputManager.instance.verticalInput) >= .1f || Mathf.Abs(InputManager.instance.horizontalInput) >= .1f)
        {
            state.SwitchState(state.RunningState);
            return;
        }
        if (_landState != null)
        {
            if (_landState.NormalizedTime >= .9f) 
            {
                _landState = null;
                state.SwitchState(state.IdlingState);
                return;
            }
        }

    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {

    }
}
