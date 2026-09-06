using System.Collections;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,

    damage,
    critChange,
    critDamage,

    hearth,
    armor,
    magicResistance,
    evasion,

    fireDamage,
    iceDamage,
    lightingDamage
}
public class CharacterStats : MonoBehaviour
{
    #region Stats
    [Header("Major stats")]
    public Stat strength;         //STR: 1 pt = 1 damage,        1 pt = .1% crit damage
    public Stat agility;          //AGI: 1 pt = .1% evasion,     1 pt = .1% crit change
    public Stat intelligence;     //INT: 1 pt = 1 magic damage,  1 pt = 3 magic resistance
    public Stat vitality;         //VIT: 1 pt = 5 HP,            1 pt = 1 physical resistance

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critDamage;       //Default value: 150%

    [Header("Defensive stats")]
    public Stat maxHearth;
    public Stat armor;
    public Stat magicResistance;
    public Stat evasion;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;
    #endregion
    #region Info
    private EntityFX fx;

    public bool isIgnited;  //Deal damage over time
    public bool isChilled;  //Reduce 20% of the target armor
    public bool isShocked;  //Reduce 20% of the target accuracy 

    [SerializeField] private float elementDuration = 4f;
    private float igNitedTimer;
    private float chillTimer;
    private float shockTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;

    public int currentHearth;
    public System.Action onHearthChange;
    public bool isDead { get; private set; }
    public bool isVulnerable { get; private set; }
    public bool isInvincible { get; private set; }
    #endregion

    protected virtual void Start()
    {
        critDamage.SetDefaultValue(150);
        currentHearth = GetMaxHealthValue();
        fx = GetComponent<EntityFX>();
    }
    protected virtual void Update()
    {
        TimeDeltaTime();
        CheckElementDuration();
        ApplyIgniteDamage();
    }

    #region Time check
    private void TimeDeltaTime()
    {
        igNitedTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;
        igniteDamageTimer -= Time.deltaTime;
    }
    private void CheckElementDuration()
    {
        if (igNitedTimer < 0)
            isIgnited = false;
        if (chillTimer < 0)
            isChilled = false;
        if (shockTimer < 0)
            isShocked = false;
    }
    #endregion
    #region Damage change
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (_targetStats.isInvincible)
            return;

        bool criticalStrike = false;

        if (TargetCanDodgeAttack(_targetStats)) return;

        _targetStats.GetComponent<Entity>().SetupKnockBackDirection(transform);

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = GetCriticalDamageValue(totalDamage);
            criticalStrike = true;
            AudioManager.instance.PlaySFX(33, null);
        }
        fx.CreateHitFX(_targetStats.transform,criticalStrike);

        totalDamage = GetTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);

        DoMagicDamage(_targetStats); // Remove if dont want MA on normal attack
    }
    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible) return;

        DecreaseHearthBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHearth <= 0 && !isDead)
            Die();
    }
    #endregion
    #region Hearth change
    public virtual void IncreaseHearthBy(int _amount)
    {
        currentHearth += _amount;

        if (currentHearth > GetMaxHealthValue())
            currentHearth = GetMaxHealthValue();

        if (onHearthChange != null)
            onHearthChange();
    }
    protected virtual void DecreaseHearthBy(int _damage)
    {
        if (isVulnerable)
            _damage = Mathf.RoundToInt(_damage * 1.1f);

        currentHearth -= _damage;

        if(_damage > 0)
            fx.CreatePopUpText(_damage.ToString());

        if (onHearthChange != null)
            onHearthChange();

    }
    #endregion
    #region Magical damage and elements
    public virtual void DoMagicDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = GetTargetMagicResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;
        AttemptyToApplyElements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }
    private void AttemptyToApplyElements(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyElement(canApplyIgnite, canApplyChill, canApplyShock);
            }
            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyElement(canApplyIgnite, canApplyChill, canApplyShock);
            }
            if (Random.value < .5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyElement(canApplyIgnite, canApplyChill, canApplyShock);
            }
        }

        if (canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        if (canApplyShock)
            _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * .1f));

        _targetStats.ApplyElement(canApplyIgnite, canApplyChill, canApplyShock);
    }
    public void ApplyElement(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;

        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            igNitedTimer = elementDuration;
            fx.IgniteFxFor(elementDuration);
        }
        if (_chill && canApplyChill)
        {
            isChilled = _chill;
            chillTimer = elementDuration;
            float slowPercentage = .2f;
            GetComponent<Entity>().SlowEntityBy(slowPercentage, elementDuration);
            fx.ChillFxFor(elementDuration);
        }
        if (_shock && canApplyShock)
        {
            if (!isShocked)
            {
                ApplyShock(_shock);
            }
            else
            {
                if (GetComponent<Player>() != null) return;
                ShockStrikeHitNearestTargetLogic();
            }
        }
    }
    public void ApplyShock(bool _shock)
    {
        if (isShocked) return;
        isShocked = _shock;
        shockTimer = elementDuration;
        fx.ShockFxFor(elementDuration);
    }
    private void ShockStrikeHitNearestTargetLogic()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
            if (!closestEnemy)
                closestEnemy = transform;
        }
        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().SetupShockStrike(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }
    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0 && isIgnited)
        {
            DecreaseHearthBy(igniteDamage);

            if (currentHearth <= 0 && !isDead)
                Die();
            igniteDamageTimer = igniteDamageCooldown;
        }
    }
    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;
    public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;
    #endregion
    #region Stats calculations
    protected virtual int GetTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= (_targetStats.armor.GetValue() + _targetStats.vitality.GetValue());

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    private int GetTargetMagicResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= (_targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3));
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }
    public virtual void OnEvasion()
    {

    }
    protected virtual bool TargetCanDodgeAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            _targetStats.OnEvasion();
            return true;
        }
        return false;
    }
    protected virtual bool CanCrit()
    {
        int totalCritChange = critChance.GetValue() + agility.GetValue();
        if (Random.Range(0, 100) <= totalCritChange)
        {
            return true;
        }
        return false;
    }
    public int GetMaxHealthValue()
    {
        return maxHearth.GetValue() + (vitality.GetValue() * 5);
    }
    protected virtual int GetCriticalDamageValue(int _damage)
    {
        float totalCritDamage = (this.critDamage.GetValue() + strength.GetValue()) * .01f;
        float critDamage = _damage * totalCritDamage;

        return Mathf.RoundToInt(critDamage);
    }
    #endregion

    public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModifier)
    {
        StartCoroutine((StatModifyCoroutine(_modifier, _duration, _statToModifier)));
    }
    public void MakeVulnerableFor(float _duration) => StartCoroutine(VulnerableCoroutine(_duration));
    private IEnumerator VulnerableCoroutine(float _duration)
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_duration);

        isVulnerable = false;
    }
    private IEnumerator StatModifyCoroutine(int _modifier, float _duration, Stat _statToModifier)
    {
        //Coroutine for buff effect, amount, duration and what Stat
        _statToModifier.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModifier.RemoveModifier(_modifier);
    }
    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;
    protected virtual void Die()
    {
        isDead = true;
    }
    public void KillEntity()
    {
        if (!isDead)
            Die();
    }
    public Stat GetStat(StatType _statType)
    {
        if (_statType == StatType.strength) return strength;
        else if (_statType == StatType.agility) return agility;
        else if (_statType == StatType.intelligence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChange) return critChance;
        else if (_statType == StatType.critDamage) return critDamage;
        else if (_statType == StatType.armor) return armor;
        else if (_statType == StatType.hearth) return maxHearth;
        else if (_statType == StatType.magicResistance) return magicResistance;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.fireDamage) return fireDamage;
        else if (_statType == StatType.iceDamage) return iceDamage;
        else if (_statType == StatType.lightingDamage) return lightingDamage;

        return null;
    }
}
