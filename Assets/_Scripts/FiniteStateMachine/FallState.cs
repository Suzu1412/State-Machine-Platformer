using UnityEngine;
using UnityEngine.Events;

public class FallState : MoveState
{
    public UnityEvent OnGrounded;
    [SerializeField] private bool isInCoyoteTime;
    [SerializeField] private float coyoteTimeDuration;

    protected override void EnterState()
    {
        fsm.Agent.AnimationManager.PlayAnimation(AnimationType.fall);

        if (fsm.Agent.MovementData.CanEnterCoyoteTime)
        {
            isInCoyoteTime = true;
            coyoteTimeDuration = fsm.Agent.Data.CoyoteDuration;
        }
    }

    public override void LogicUpdate()
    {
        CalculateVelocity();

        if (isInCoyoteTime)
        {
            coyoteTimeDuration -= Time.deltaTime;

            if (coyoteTimeDuration <= 0f)
            {
                isInCoyoteTime = false;
                fsm.Agent.MovementData.ConsumeJump();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        fsm.Agent.CollissionSenses.DetectGround();
        fsm.Agent.CollissionSenses.DetectWall();
        fsm.Agent.CollissionSenses.DetectLadder();

        SetPlayerVelocity();

        if (fsm.Agent.CollissionSenses.IsGrounded)
        {
            if (fsm.Agent.Rb2d.velocity.y < 0) return;
            if (Mathf.Abs(fsm.Agent.Rb2d.velocity.x) > 0f)
            {
                fsm.TransitionToState(StateType.Move);
            }
            else
            {
                fsm.TransitionToState(StateType.Idle);
            }
        }

        ClimbLadder();
    }

    protected override void ExitState()
    {
        if (!fsm.Agent.CollissionSenses.IsGrounded) return;

        OnGrounded?.Invoke();
        fsm.Agent.MovementData.ResetJump(fsm.Agent);
    }

    protected override void HandleRollPressed()
    {

    }
}
