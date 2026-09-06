using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum RewardType
{
    Merchant,
    Blacksmith,
    None
}
[CreateAssetMenu(menuName = "RPG Setup/ Quest Data/ New Quest" , fileName = "Quest - ")]
public class QuestDataSO : ScriptableObject
{
    public string questSaveId;
    [Space]
    public string questName;
    [TextArea] public string questDescription;
    [TextArea] public string questGoal;

    public string questTargetId; // Enemy name, NPC name, Itemname
    public int requitedAmount;

    [Header("Reward")]
    public RewardType rewardType;
    public InventoryItem[] rewardItems;


    private void OnValidate()
    {
#if UNITY_EDITOR
    string path = AssetDatabase.GetAssetPath(this);
    questSaveId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
        
}
