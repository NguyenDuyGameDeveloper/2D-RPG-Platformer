using UnityEngine;
using UnityEngine.UI;

public class BlackholeSkill : Skill
{
    #region Info
    [SerializeField] private UI_SkillTreeSlot blackHoleUnlockButton;
    public bool blackHoleUnlocked {  get; private set; }

    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private float blackHoleDuration;
    [Space]
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    Blackhole_Skill_Controller currentBlackHole;
    #endregion

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }
    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, Quaternion.identity);

        currentBlackHole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();
        currentBlackHole.SetupBlackHole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneAttackCooldown,blackHoleDuration);
    }
    protected override void Start()
    {
        base.Start();

        blackHoleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackHole);
    }
    protected override void Update()
    {
        base.Update();
    }
    private void UnlockBlackHole()
    {
        if (blackHoleUnlockButton.unlocked)
            blackHoleUnlocked = true;
    }
    public bool SkillComplete()
    {
        if (!currentBlackHole) return false;

        if (currentBlackHole.playerCanExitState)
        {
            currentBlackHole = null;
            return true;
        }
        return false;
    }
    public float GetBlackHoleRadius()
    {
        return maxSize / 2; 
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();

        UnlockBlackHole();
    }
}
