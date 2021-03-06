using UnityEngine;

public class MarioOverworldSpinAndJumpState : MarioOverworldBaseState, IMarioOverworldRootState
{
    public MarioOverworldSpinAndJumpState(MarioOverworldStateMachine currentContext, MarioOverworldStateFactory marioOverworldStateFactory) 
        : base(currentContext, marioOverworldStateFactory) {}
    
    public override void EnterState()
    {
        _ctx.Animator.Play("m_spin_and_jump");
        _ctx.Velocity = _ctx.Gravity;
        _isRootState = true;
        InitializeSubState();
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {}

    public override void CheckSwitchStates()
    {
        
        if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            SwitchState(_factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        // Don't
    }

    public override void AnimateState()
    {
        // Don't
    }

    public void HandleGravity()
    {
        _ctx.Controller.Move(new Vector3(0f, _ctx.Velocity * Time.deltaTime));
    }
}
