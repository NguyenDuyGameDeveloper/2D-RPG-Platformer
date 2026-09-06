using UnityEngine;

public class ShockStrike_Controller : MonoBehaviour
{
    private CharacterStats targetStats;
    private Animator anim;

    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private bool triggered;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Check();
        MoveTowardTarget();
        DistanceAndLocalScale();
    }
    private void Check()
    {
        if (!targetStats) return;
        if (triggered) return;
    }
    private void MoveTowardTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;
    }
    private void DistanceAndLocalScale()
    {
        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {
            anim.transform.localPosition = new Vector3(0, .5f);
            anim.transform.localRotation = Quaternion.identity;

            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Invoke(nameof(DamageAndDestroy), .2f);
            triggered = true;
            anim.SetTrigger("Hit");
        }
    }
    public void SetupShockStrike(int _damage, CharacterStats _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }
    private void DamageAndDestroy()
    {
        targetStats.ApplyShock(true);
        targetStats.TakeDamage(damage);
        Destroy(gameObject, .1f);
    }
}