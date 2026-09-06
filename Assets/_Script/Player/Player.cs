using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region Info
    //[Header("Player Input System")]
    public PlayerInputSet input { get; private set; }
    public UI ui { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    public float swordReturnImpact;
    private float defaultMoveSpeed;
    private float defaultJumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    private float defaultDashSpeed;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }
    public PlayerFX playerFX { get; private set; }
    #endregion
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    //Attack and Skill
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }
    public PlayerBlackHoleState blackHole { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        ui = FindAnyObjectByType<UI>();
        
        input = new PlayerInputSet();
        ui.SetupControlUI(input);

        stateMachine = new PlayerStateMachine();
        //Movement State
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
        //Normal Attack State
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        //Skill State
        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHole = new PlayerBlackHoleState(this, stateMachine, "Jump");
    }
    protected override void Start()
    {
        base.Start();
        playerFX = GetComponent<PlayerFX>();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }
    protected override void Update()
    {
        if (Time.timeScale == 0) return;

        base.Update();
        stateMachine.currentState.Update();
        this.CheckForInput();
    }
    public override void SlowEntityBy(float _slowPersentage, float _slowDuration)
    {
        moveSpeed *= (1 - _slowPersentage);
        jumpForce *= (1 - _slowPersentage);
        dashSpeed *= (1 - _slowPersentage);
        anim.speed = anim.speed * (1 - _slowPersentage);

        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

    }
    public void AssignNewSword(GameObject _newSword) => sword = _newSword;
    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && skill.crystal.crystalUnlocked)
            skill.crystal.CanUseSkill();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.instance.UsePotion();

        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill() && SkillManager.instance.dash.dashUnlocked)
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }
    protected override void SetupZeroKnockBackPower()
    {
        knockbackPower = new Vector2(0, 0);
    }
}
