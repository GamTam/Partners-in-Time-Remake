using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOverworldWalkState : EnemyOverworldBaseState
{
    private Vector3 _newMove;

    public EnemyOverworldWalkState(EnemyOverworldStateMachine currentContext, EnemyOverworldStateFactory enemyOverworldStateFactory) 
        : base(currentContext, enemyOverworldStateFactory) {}
    
    public override void EnterState() {
        _newMove = new Vector3(0f, 0f, 0f);
    }

    public override void UpdateState()
    {
        float targetAngle = Mathf.Atan2(_ctx.MoveVector.x, _ctx.MoveVector.z) * Mathf.Rad2Deg + _ctx.Cam.eulerAngles.y;
        _ctx.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        _newMove = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        _ctx.MoveAngle = targetAngle;
        _newMove = _newMove * _ctx.MoveSpeed * Time.deltaTime;

        _ctx.Controller.Move(_newMove);

        CheckSwitchStates();
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

    public override void AnimateState()
    {
        _ctx.Animator.Play(_ctx.AnimPrefix + "_walk" + _ctx.Facing);
    }
}