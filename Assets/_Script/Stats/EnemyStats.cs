using UnityEngine;

public class EnemyStats : CharacterStats
{
    #region Info
    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat goldDropAmount;
    [HideInInspector]
    public bool killByPlayer = false;

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float persentagedModifier = .4f;
    #endregion
    protected override void Start()
    {
        ApplyModifiers();
        //goldDropAmount.SetDefaultValue(10);
        base.Start();
        SetComponents();
    }
    private void SetComponents()
    {
        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
    }
    private void ApplyModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critDamage);

        Modify(maxHearth);
        Modify(armor);
        Modify(magicResistance);
        Modify(evasion);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

        Modify(goldDropAmount);
    }
    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * persentagedModifier;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        killByPlayer = true;
        //Debug.Log("TakeDamage: " + killByPlayer);
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();

        if (killByPlayer == true)
        {
            AddGoldToPlayerWhenDie();

            //Debug.Log("Die: " + killByPlayer);
        }

        myDropSystem.GenerateDrop();

        Destroy(gameObject, 5f);
    }
    public void AddGoldToPlayerWhenDie() => PlayerManager.instance.golds += goldDropAmount.GetValue();
}
