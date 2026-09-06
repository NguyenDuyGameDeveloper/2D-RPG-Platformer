using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region State
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion

    //[Header("Detection (tuneable)")]
    //[SerializeField] private Vector2 detectBoxSize = new Vector2(3f, 2f); // width (x) = distance across, height (y) = vertical window
    //[SerializeField] private float detectBoxForwardOffset = 1f;         // offset forward from wallCheck
    //[SerializeField] private LayerMask detectionMask;                   // optional override, default to whatIsPlayer
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Battle", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    //protected override void OnDrawGizmos()
    //{
    //    PositionCheck();
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - 0.4f), new Vector3(transform.position.x + attackDistance * facingDir + 2, transform.position.y - 0.4f));
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(attackCheck.position, new Vector3(attackCheck.position.x + 2,attackCheck.position.y +1));
    //}
    //protected override void PositionCheck()
    //{
    //    base.PositionCheck();
    //}
    //public override RaycastHit2D IsPlayerDetected()
    //{
    //    //return base.IsPlayerDetected();

    //    Vector2 origin = wallCheck.position;
    //    // center of the box: forward from wallCheck by half the detection width plus custom offset
    //    float halfWidth = detectBoxSize.x * 0.5f;
    //    Vector2 boxCenter = (Vector2)origin + Vector2.right * facingDir * (halfWidth + detectBoxForwardOffset);

    //    // Check any collider of player layer inside box
    //    Collider2D found = Physics2D.OverlapBox(boxCenter, detectBoxSize, 0f, detectionMask);

    //    if (found != null)
    //    {
    //        // Return a RaycastHit2D that represents the hit distance from wallCheck to the collider center/point
    //        Vector2 dir = ((Vector2)found.bounds.center - origin).normalized;
    //        float maxDistance = Vector2.Distance(origin, found.bounds.center) + Mathf.Max(found.bounds.extents.x, found.bounds.extents.y);
    //        RaycastHit2D rayHit = Physics2D.Raycast(origin, dir, maxDistance, detectionMask);
    //        // If raycast fails for some reason, create a fallback RaycastHit2D using collider info
    //        if (!rayHit && found != null)
    //        {
    //            // Create a minimal RaycastHit2D-like result (distance  = distance to center)
    //            // Note: constructing RaycastHit2D manually won't set collider directly, so we return the rayHit if available.
    //            // Fallback: attempt a short ray towards the collider center
    //            rayHit = Physics2D.Raycast(origin, dir, maxDistance, detectionMask);
    //        }
    //        return rayHit;
    //    }

    //    return default;
    //}
    //private void OnDrawGizmosSelected()
    //{
    //    if (wallCheck == null) return;
    //    Gizmos.color = Color.cyan;
    //    float halfWidth = detectBoxSize.x * 0.5f;
    //    Vector2 boxCenter = (Vector2)wallCheck.position + Vector2.right * facingDir * (halfWidth + detectBoxForwardOffset);
    //    Gizmos.DrawWireCube(boxCenter, detectBoxSize);
    //}
}
