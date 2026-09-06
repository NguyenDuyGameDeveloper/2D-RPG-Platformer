using System.Collections;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    #region Info
    private Player player;
    private SpriteRenderer sr;
    private Animator anim;

    //[SerializeField] private float cloneDuration;
    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    [Space]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float checkClosetEnemyRadius = 25;
    [SerializeField] private Transform closestEnemy;
    private bool canDuplicateClone;
    private int facingDir = 1;

    private float cloneTimer;
    private float changeToDuplicate;
    private float cloneAttackMultiplier;
    #endregion
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        StartCoroutine(nameof(FaceClosestTargetCoroutine));
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _newTransform, float _cloneDuration,
        bool _canAttack, Vector3 _offset, bool _canDuplicate,
        float _changeToDuplicate, Player _player, float _cloneAttackMultiplier)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));

        player = _player;
        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneDuration;
        canDuplicateClone = _canDuplicate;
        changeToDuplicate = _changeToDuplicate;
        cloneAttackMultiplier = _cloneAttackMultiplier;
    }
    public void AnimationTrigger() => cloneTimer = -0.1f;
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().SetupKnockBackDirection(transform);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();

                playerStats.CloneDoDamage(enemyStats, cloneAttackMultiplier);

                if (player.skill.clone.canApplyOnHitEffectUnlocked)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null)
                        weaponData.Effect(hit.transform);
                }

                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < changeToDuplicate)
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0));
                }
            }
        }
    }
    private IEnumerator FaceClosestTargetCoroutine()
    {
        yield return null;
        FindClosestEnemy();

        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
    private void FindClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkClosetEnemyRadius, whatIsEnemy);

        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = hit.transform;
            }
        }
    }
}
