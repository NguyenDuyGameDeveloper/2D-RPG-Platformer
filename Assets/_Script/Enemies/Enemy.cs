using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    #region Info
    [Header("Quest Info")]
    public string questTargetId;
    
    public EntityFX fx { get; private set; }

    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned info")]
    public float stunDuration = 1f;
    public Vector2 stunDirection = new(10, 12);
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2f;
    public float battleTime = 7f;
    private float defaultMoveSpeed;

    [Header("Attack info")]
    public float agroDistance = 2f;
    public float attackDistance = 2f;
    public float attackCooldown;
    public float minAttackCooldown = 1f;
    public float maxAttackCooldown = 2f;
    [HideInInspector] public float lastTimeAttacked;
    public string lastAnimBoolName { get; private set; }
    #endregion
    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
        fx = GetComponent<EntityFX>();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public override void SlowEntityBy(float _slowPersentage, float _slowDuration)
    {
        moveSpeed *= (1 - _slowPersentage);
        anim.speed = anim.speed * (1 - _slowPersentage);

        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
    }
    public virtual void AssignLastAnimName(string _animBoolName) => this.lastAnimBoolName = _animBoolName;
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }
    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimeCoroutine(_duration));
    protected virtual IEnumerator FreezeTimeCoroutine(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }
    #region Window Counter Attack
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    #endregion
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual void AnimationSpecialAttackTrigger()
    {

    }
    public virtual RaycastHit2D IsPlayerDetected()
    {
        RaycastHit2D playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 10, whatIsPlayer);
        RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 10, whatIsGround);

        if (wallDetected)
        {
            if (wallDetected.distance < playerDetected.distance)
                return default;
        }

        return playerDetected;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - 0.4f), new Vector3(transform.position.x + attackDistance * facingDir + 2, transform.position.y - 0.4f));
    }
}
