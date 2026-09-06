using UnityEngine;

[CreateAssetMenu(fileName = "Freeze Enemy Effect", menuName = "Data/Item Effect/Freeze Enemy Effect")]
public class FreezeEnemiesEffect : ItemEffect
{
    [SerializeField] private float duration;

    public override void ExecuteEffect(Transform _transform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats.currentHearth > playerStats.GetMaxHealthValue() * .1f) return;
        if (!Inventory.instance.CanUseArmorEffect()) return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);
        foreach (var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
        }
    }
}
