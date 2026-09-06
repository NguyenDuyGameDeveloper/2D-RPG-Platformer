using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloneSkill : Skill
{
    #region Info
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float attackMultiplier;
    [SerializeField] private float cloneDuration;

    [Header("Clone can attack")]
    [SerializeField] private UI_SkillTreeSlot cloneCanAttackUnlockButton;
    [SerializeField] private float cloneAttackMultiplier;
    public bool canAttackUnlocked { get; private set; }

    [Header("Aggresive clone")]
    [SerializeField] private UI_SkillTreeSlot cloneAggesiveUnlockButton;
    [SerializeField] private float aggresiveCloneAttackMultiplier;
    public bool canApplyOnHitEffectUnlocked { get; private set; }

    [Header("Multiple clone")]
    [SerializeField] private UI_SkillTreeSlot cloneMultipleUnlockButton;
    [SerializeField] private float multiCloneAttackMultiplier;
    [SerializeField] private float changeToDuplicate;
    public bool canDuplicateCloneUnlocked { get; private set; }

    [Header("Crystal instead of clone")]
    [SerializeField] private UI_SkillTreeSlot crystalInsteatOfCloneUnlockButton;
    public bool crystalInsteadOfCloneUnlocked { get; private set; }
    #endregion
    protected override void Start()
    {
        base.Start();

        cloneCanAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneCanAttack);
        cloneAggesiveUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggresiveMirage);
        cloneMultipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultipleClone);
        crystalInsteatOfCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInsteadOfClone);
    }
    #region Unlock skill
    protected override void CheckUnlock()
    {
        UnlockCloneCanAttack();
        UnlockAggresiveMirage();
        UnlockMultipleClone();
        UnlockCrystalInsteadOfClone();
    }
    private void UnlockCloneCanAttack()
    {
        if(cloneCanAttackUnlockButton.unlocked)
        {
            canAttackUnlocked = true;
            attackMultiplier = cloneAttackMultiplier;
        }
    }
    private void UnlockAggresiveMirage()
    {
        if (cloneAggesiveUnlockButton.unlocked)
        {
            canApplyOnHitEffectUnlocked = true;
            attackMultiplier = aggresiveCloneAttackMultiplier;
        }

    }
    private void UnlockMultipleClone()
    {
        if (cloneMultipleUnlockButton.unlocked)
        {
            canDuplicateCloneUnlocked = true;
            attackMultiplier = multiCloneAttackMultiplier;
        }

    }
    private void UnlockCrystalInsteadOfClone()
    {
        if (crystalInsteatOfCloneUnlockButton.unlocked)
            crystalInsteadOfCloneUnlocked = true;

    }
    #endregion
    #region Create clone
    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (crystalInsteadOfCloneUnlocked)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().
            SetupClone(_clonePosition, cloneDuration, canAttackUnlocked, _offset, canDuplicateCloneUnlocked, changeToDuplicate, player,cloneAttackMultiplier);
    }
    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCoroutine(_enemyTransform, new Vector3(player.facingDir * 2, 0)));
    }
    #endregion
    private IEnumerator CloneDelayCoroutine(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, _offset);
    }
}
