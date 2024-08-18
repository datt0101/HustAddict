using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Animancer;

public class IdlingState : OnGroundState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] ClipTransition _clip;
    [SerializeField] ClipTransition _stopClip;
    [SerializeField] ClipTransition _stopWalkClip;
    AnimancerState _stopState;
    AnimancerState _stopWalkState;
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
        if (StateManager.instance.previousState == StateManager.instance.RunningState)
        {
            _stopWalkState = _animancer.Play(_stopWalkClip, .25f);
            _stopWalkState.Speed = 2f;
        }
        else if(StateManager.instance.previousState == StateManager.instance.SprintingState)
        {
            _stopState = _animancer.Play(_stopClip, .25f);
        }
        else
        {
            _animancer.Play(_clip, .25f);
        }
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
        if(_stopState!=null)
        {
            if (_stopState.NormalizedTime >= .9f)
            {
                _stopState = null;
                state.SwitchState(state.IdlingState);
                return;
            }
        }
        if (_stopWalkState != null)
        {
            if (_stopWalkState.NormalizedTime >= .9f)
            {
                _stopWalkState = null;
                state.SwitchState(state.IdlingState);
                return;
            }
        }
        if(Mathf.Abs(InputManager.instance.verticalInput) >= .1f || Mathf.Abs(InputManager.instance.horizontalInput) >= .1f)
        {
            state.SwitchState(state.RunningState);
            return;
        }
    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {
        
    }
}
