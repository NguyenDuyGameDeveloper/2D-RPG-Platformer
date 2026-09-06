using UnityEngine;
using UnityEngine.UI;

public class ParrySkill : Skill
{
    #region Info
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockedButton;
    public bool parryUnlocked { get; private set; }

    [Header("Parry restore")]
    [SerializeField] private UI_SkillTreeSlot parryRestoreUnlockedButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;
    public bool parryRestoreUnlocked { get; private set; }


    [Header("Parry with mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockedButton;
    public bool parryWithMirageUnlocked { get; private set; }
    #endregion
    public override void UseSkill()
    {
        base.UseSkill();

        if (parryRestoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthAmount);
            player.stats.IncreaseHearthBy(restoreAmount);
        }
    }
    protected override void Start()
    {
        base.Start();

        parryUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        parryRestoreUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }
    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
    }
    private void UnlockParry()
    {
        if (parryUnlockedButton.unlocked)
            parryUnlocked = true;
    }
    private void UnlockParryRestore()
    {
        if (parryRestoreUnlockedButton.unlocked)
            parryRestoreUnlocked = true;
    }
    private void UnlockParryWithMirage()
    {
        if (parryWithMirageUnlockedButton.unlocked)
            parryWithMirageUnlocked = true;
    }
    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }
}
