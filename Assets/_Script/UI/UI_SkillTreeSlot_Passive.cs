using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTreeSlot_Passive : UI_SkillTreeSlot
{
    [Header("Passive")]
    [SerializeField] private GameObject iconImage;

    protected override void Start()
    {
        ui = GetComponentInParent<UI>();

        CheckImage();
    }
    protected override void UnlockSkillSlot()
    {
        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (!shouldBeUnlocked[i].unlocked)
            {
                Debug.Log("Cannot unlock skill!");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked)
            {
                Debug.Log("Cannot unlock skill!");
                return;
            }
        }
        if (AlreadyUnlockSkill()) return;

        if (!PlayerManager.instance.HaveEnoughSkillPoint(skillPoint,this)) return;

        Image icon = iconImage.GetComponent<Image>();

        unlocked = true;
        icon.color = Color.white;
        PlayerManager.instance.unlockedSkills.Add(this);
    }
    private void CheckImage()
    {
        if (iconImage != null)
        {
            Image icon = iconImage.GetComponent<Image>();
            if (icon != null)
            {
                icon.color = lockedSkillColor;
            }
        }
    }
}
