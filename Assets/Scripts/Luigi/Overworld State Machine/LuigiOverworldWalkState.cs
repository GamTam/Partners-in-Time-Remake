using UnityEngine;

public class LuigiOverworldWalkState : LuigiOverworldBaseState
{
    private Vector3 _newMove;
    
    public LuigiOverworldWalkState(LuigiOverworldStateMachine currentContext, LuigiOverworldStateFactory LuigiOverworldStateFactory) 
        : base(currentContext, LuigiOverworldStateFactory) {}
    
    public override void EnterState()
    {
        _newMove = new Vector3(0f, 0f, 0f);
    }

    public override void UpdateState()
    {
        _ctx.PosQueue.Enqueue(_ctx.MarioPos.position);
        _ctx.RotQueue.Enqueue(_ctx.MarioPos.rotation);

        if (_ctx.PosQueue.Count >= _ctx.QueueDelay)
        {
            Vector3 pos = _ctx.PosQueue.Dequeue();

            _newMove.x = pos.x - _ctx.transform.position.x;
            _newMove.z = pos.z - _ctx.transform.position.z;

            float targetAngle = Mathf.Atan2(_newMove.x, _newMove.z) * Mathf.Rad2Deg;
            _ctx.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            _ctx.MoveAngle = targetAngle;
        }

        _ctx.Controller.Move(_newMove * _ctx.MoveSpeed * Time.deltaTime);
        
        CheckSwitchStates();
    }

    public override void FixedUpdate()
    {
        
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.MoveVector.magnitude < Globals.deadZone)
        {
            SwitchState(_factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        
    }

    public override void AnimateState()
    {
        _ctx.Animator.Play("l_walk" + _ctx.Facing);
    }
}
