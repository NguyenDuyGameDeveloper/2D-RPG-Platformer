using UnityEngine;
using UnityEngine.UI;

public class DodgeSkill : Skill
{
    #region Info
    [Header("Dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] private int evasionAmount;
    public bool dodgeUnlocked { get; private set; }

    [Header("Dodge mirage")]
    [SerializeField] private UI_SkillTreeSlot unlockDodgeMirageButton;
    public bool dodgeMirageUnlocked { get; private set; }
    #endregion
    protected override void Start()
    {
        base.Start();

        unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockDodgeMirageButton.GetComponent<Button>().onClick.AddListener(UnlockDodgeMirage);
    }
    protected override void CheckUnlock()
    {
        UnlockDodge();
        UnlockDodgeMirage();
    }
    private void UnlockDodge()
    {
        if (unlockDodgeButton.unlocked && !dodgeUnlocked)
        {
            player.stats.evasion.AddModifier(evasionAmount);
            Inventory.instance.UpdateStatsUI();
            dodgeUnlocked = true;
        }
    }
    private void UnlockDodgeMirage()
    {
        if (unlockDodgeMirageButton.unlocked)
            dodgeMirageUnlocked = true;
    }
    public void CreateMirageOnDodge()
    {
        if (dodgeMirageUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, new Vector3(2 * player.facingDir, 0));
    }
}
