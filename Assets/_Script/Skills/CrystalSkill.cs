using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalSkill : Skill
{
    #region Info
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;


    [Header("Crystal simple")]
    [SerializeField] private UI_SkillTreeSlot unlockCrystalButton;
    public bool crystalUnlocked {  get; private set; }

    [Header("Mirage crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockCloneInsteadButton;
    public bool cloneInsteadOfCrystalUnlocked { get; private set; }

    [Header("Explosive crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockExplosiveButton;
    public bool explosiveUnlocked { get; private set; }

    [Header("Moving crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMovingCrystalButton;
    [SerializeField] private float moveSpeed;
    public bool movingUnlocked { get; private set; }

    [Header("Multi stacking crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMultiStackButton;
    [SerializeField] public int amountOfStacks;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    public bool multipleCrystalUnlocked { get; private set; }

    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();
    #endregion
    protected override void Start()
    {
        base.Start();

        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockCloneInsteadButton.GetComponent<Button>().onClick.AddListener(UnlockMirage);
        unlockExplosiveButton.GetComponent<Button>().onClick.AddListener(UnlockExplosive);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiStackButton.GetComponent<Button>().onClick.AddListener(UnlockMultipleCrystal);

    }
    #region Unlock Skill
    protected override void CheckUnlock()
    {
        UnlockCrystal();
        UnlockMirage();
        UnlockExplosive();
        UnlockMovingCrystal();
        UnlockMultipleCrystal();
    }
    private void UnlockCrystal()
    {
        if(unlockCrystalButton.unlocked)
            crystalUnlocked = true;
    }
    private void UnlockMirage()
    {
        if (unlockCloneInsteadButton.unlocked)
            cloneInsteadOfCrystalUnlocked = true;
    }
    private void UnlockExplosive()
    {
        if (unlockExplosiveButton.unlocked)
            explosiveUnlocked = true;
    }
    private void UnlockMovingCrystal()
    {
        if (unlockMovingCrystalButton.unlocked)
            movingUnlocked = true;
    }
    private void UnlockMultipleCrystal()
    {
        if (unlockMultiStackButton.unlocked)
            multipleCrystalUnlocked = true;
    }
    #endregion
    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;
        CheckList_CanCreateNewCrystal();
    }
    private void CheckList_CanCreateNewCrystal()
    {
        if (currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (movingUnlocked) return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (cloneInsteadOfCrystalUnlocked)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
            }
        }
    }
    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller newCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        newCrystalScript.SetupCrystal(crystalDuration, explosiveUnlocked, movingUnlocked, moveSpeed, FindClosestEnemy(currentCrystal.transform),player);
    }
    public void CurrentCrystalChooseRandomEnemy() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();
    private bool CanUseMultiCrystal()
    {
        if (multipleCrystalUnlocked)
        {
            if (crystalLeft.Count > 0)
            {
                if (crystalLeft.Count == amountOfStacks)
                    Invoke(nameof(ResetSkill), useTimeWindow);

                coolDown = 0;
                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                GameObject newCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
                crystalLeft.Remove(crystalToSpawn);
                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, explosiveUnlocked, movingUnlocked, moveSpeed,
                    FindClosestEnemy(newCrystal.transform),player);

                if (crystalLeft.Count <= 0)
                {
                    coolDown = multiStackCooldown;
                    RefillCrystal();
                }
                return true;
            }
        }
        return false;
    }
    private void RefillCrystal()
    {
        int amountToAdd = amountOfStacks - crystalLeft.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }
    private void ResetSkill()
    {
        if (coolDownTimer > 0) return;

        coolDownTimer = multiStackCooldown;
        RefillCrystal();
    }
}
