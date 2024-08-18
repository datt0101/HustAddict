using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
public class FallingState : OnAirState
{
    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] ClipTransition _clip;
    public override void EnterState(StateManager state)
    {
        base.EnterState(state);
        _animancer.Play(_clip, .25f);
    }
    public override void ExitState(StateManager state)
    {
        base.ExitState(state);
    }
    public override void UpdateState(StateManager state)
    {
        base.UpdateState(state);
    }
    public override void OnCollisionEnt(StateManager state, Collision collision)
    {

    }
}
