using TMPro;
using UnityEngine;

public class UI_SkillTreeSwitcher : MonoBehaviour
{
    [SerializeField] private UI_SkillTree activeSkillTree;
    [SerializeField] private UI_SkillTree passiveSkillTree;

    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        UpdateButtonText();
    }
    private void FixedUpdate()
    {
        if (activeSkillTree.gameObject.activeSelf)
            passiveSkillTree.gameObject.SetActive(false);
        else if (passiveSkillTree.gameObject.activeSelf) 
            passiveSkillTree.gameObject.SetActive(true);
    }

    public void SwitchSkillTree()
    {
        // Nếu Skill Tree Active đang hiển thị, chuyển sang Passive và ngược lại
        if (activeSkillTree.gameObject.activeSelf)
        {
            activeSkillTree.gameObject.SetActive(false);
            passiveSkillTree.gameObject.SetActive(true);
        }
        else
        {
            activeSkillTree.gameObject.SetActive(true);
            passiveSkillTree.gameObject.SetActive(false);
        }
        UpdateButtonText();
    }
    private void UpdateButtonText()
    {
        if (activeSkillTree.gameObject.activeSelf)
        {
            text.text = "Passive"; // Nếu Active Skill Tree đang hiển thị, thay đổi nội dung của Button thành "Passive"
        }
        else if (passiveSkillTree.gameObject.activeSelf)
        {
            text.text = "Active"; // Nếu Passive Skill Tree đang hiển thị, thay đổi nội dung của Button thành "Active"
        }
    }
}
