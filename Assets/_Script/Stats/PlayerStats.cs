using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }
    protected override void Die()
    {
        base.Die();
        player.Die();

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }
    protected override void DecreaseHearthBy(int _damage)
    {
        base.DecreaseHearthBy(_damage);
        if (isDead) return;

        if (Mathf.RoundToInt(_damage) >= (GetMaxHealthValue() * 0.2f))
        {
            player.SetupKnockBackPower(new Vector2(8, 6));
            player.playerFX.ScreenShake(player.playerFX.shakeHighDamage);
            AudioManager.instance.PlaySFX(32, null);
        }

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        if (currentArmor != null)
            currentArmor.Effect(player.transform);
    }
    public override void OnEvasion()
    {
        player.skill.dodge.CreateMirageOnDodge();
    }
    public void CloneDoDamage(CharacterStats _targetStats, float _cloneAttackMultiplier)
    {
        if (TargetCanDodgeAttack(_targetStats)) return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (_cloneAttackMultiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * _cloneAttackMultiplier);

        if (CanCrit())
            totalDamage = GetCriticalDamageValue(totalDamage);

        totalDamage = GetTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }
}
