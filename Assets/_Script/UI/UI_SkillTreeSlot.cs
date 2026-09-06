using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    protected UI ui;
    protected Image skillImage;

    public bool unlocked;
    [SerializeField] protected int skillPoint;
    [SerializeField] protected string skillName;
    [TextArea]
    [SerializeField] protected string skillDescription;
    [SerializeField] protected Color lockedSkillColor;


    [SerializeField] protected UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] protected UI_SkillTreeSlot[] shouldBeLocked;


    public virtual void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }
    protected virtual void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }
    protected virtual void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;

        if(unlocked)
            skillImage.color = Color.white;
    }
    protected virtual void UnlockSkillSlot()
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

        if (PlayerManager.instance.HaveEnoughSkillPoint(skillPoint, this) == false) return;

        unlocked = true;
        skillImage.color = Color.white;
        PlayerManager.instance.unlockedSkills.Add(this);
    }
    protected virtual bool AlreadyUnlockSkill()
    {
        if (unlocked)
        {
            Debug.Log("Skill already unlocked!");
            return true;
        }
        return false;
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription, skillName, skillPoint);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }
    public void LoadData(GameData _data) //Error
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
            unlocked = value;

    }
    public void SaveData(ref GameData _data) //Error
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);
    }
}
