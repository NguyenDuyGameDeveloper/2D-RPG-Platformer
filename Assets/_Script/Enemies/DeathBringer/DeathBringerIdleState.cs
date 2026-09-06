using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    protected Enemy_DeathBringer enemy;
    protected Transform player;
    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Vector2.Distance(player.transform.position, enemy.transform.position) < 7 )
        {
            enemy.bossFightBegin = true;
        }

        if (Input.GetKeyDown(KeyCode.V))
            stateMachine.ChangeState(enemy.teleportState);

        if (stateTimer < 0 && enemy.bossFightBegin)
            stateMachine.ChangeState(enemy.battleState);
    }
}
