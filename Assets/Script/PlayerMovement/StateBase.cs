using EasyCharacterMovement;
using UnityEngine;

public  class StateBase: MonoBehaviour
{
    protected float elapsedTime = 0;
    protected StateManager stateManager;
    [SerializeField] protected CharacterMovement characterMovement;
    [SerializeField] protected CharacterControlFake characterControl;
    public virtual  void EnterState(StateManager state)
    {
        elapsedTime = 0;
        stateManager = state;
        characterMovement = state.player.GetComponent<CharacterMovement>();
        characterControl = state.player.GetComponent<CharacterControlFake>();
    }
    public virtual void ExitState(StateManager state)
    {

    }
    public  virtual void UpdateState(StateManager state)
    {
        elapsedTime += Time.deltaTime;
    }
    public  virtual void OnCollisionEnt(StateManager state, Collision collision)
    {

    }
}
