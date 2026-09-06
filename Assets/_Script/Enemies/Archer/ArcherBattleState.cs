using UnityEngine;

public class ArcherBattleState : EnemyState
{
    protected Enemy_Archer enemy;
    protected Transform player;
    protected int moveDir;

    public ArcherBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Archer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsGroundDetected() == false)
            enemy.stateMachine.ChangeState(enemy.moveState);

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            JumpIfPlayerIsNear();
            Attack();
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
        }

        BattleStateFlipControl();
    }
    private void BattleStateFlipControl()
    {
        if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
    }
    private void Attack()
    {
        if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
        {
            if (CanAttack())
                stateMachine.ChangeState(enemy.attackState);
        }
    }
    private void JumpIfPlayerIsNear()
    {
        if (enemy.IsPlayerDetected().distance < enemy.safeDistance)
        {
            if (CheckCanJump())
                stateMachine.ChangeState(enemy.jumpState);
        }
    }
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
    public bool CheckCanJump()
    {
        if (enemy.GroundBehind() == false || enemy.WallBehind() == true)
            return false;

        if (Time.time >= enemy.lastTimeJump + enemy.jumpCooldown)
        {
            enemy.lastTimeJump = Time.time;
            return true;
        }
        return false;
    }
}
