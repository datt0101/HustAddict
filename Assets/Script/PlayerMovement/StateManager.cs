using Animancer;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public StateBase currentState;
    public StateBase previousState;
    public IdlingState IdlingState;
    public RunningState RunningState;
    public FallingState FallingState;
    public LandingState LandingState;
    public SprintState SprintingState;
    public JumpingState JumpingState;
    public GameObject player;
    public AnimancerComponent animancerComponent;
    public AvatarMask mask;
    [SerializeField] ClipState _clipState;
    CharacterMovement _characterMovement;
    private void Awake()
    {
        _characterMovement = player.GetComponent<CharacterMovement>();
        animancerComponent.Layers[1].SetMask(mask);
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        _characterMovement.enablePhysicsInteraction = true;
        _characterMovement.physicsInteractionAffectsCharacters = true;
        _characterMovement.impartPlatformMovement = true;
        _characterMovement.impartPlatformRotation = true;
        _characterMovement.impartPlatformVelocity = true;
        currentState = IdlingState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
        //Debug.LogWarning(currentState.name);
    }
    public void SwitchState(StateBase state)
    {
        currentState.ExitState(this);
        previousState = currentState;
        currentState = state;
        state.EnterState(this);
    }
}
