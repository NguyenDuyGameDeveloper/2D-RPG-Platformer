using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    #region Info
    public List<UI_SkillTreeSlot> unlockedSkills = new List<UI_SkillTreeSlot>();
    public static PlayerManager instance;
    public Player player;

    [Header("Currency")]
    public int skillPoints;
    public int golds;
    #endregion

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    public bool HaveEnoughSkillPoint(int _price,UI_SkillTreeSlot _skillSlot)
    {
        if (_price > skillPoints)
        {
            Debug.Log("Not enought Skill Point");
            return false;
        }
        if(unlockedSkills.Contains(_skillSlot))
        {
            Debug.Log("Skill adready unlock!");
            return false;
        }

        skillPoints -= _price;
        return true;
    }
    public int GetSkillPoint() => skillPoints;
    public int GetGold() => golds;
    public void LoadData(GameData _data)
    {
        this.golds = _data.golds;
        this.skillPoints = _data.skillPoints;
    }
    public void SaveData(ref GameData _data)
    {
        _data.golds = this.golds;
        _data.skillPoints = this.skillPoints;
    }
}
