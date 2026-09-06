using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/ Quest Data/ Quest Database", fileName = "QUEST DATABASE")]
public class QuestDataBaseSO : ScriptableObject
{
    public QuestDataSO[] allQuests;

    public QuestDataSO GetQuestDataSO(string _id)
    {
        return allQuests.FirstOrDefault(q => q != null && q.questTargetId == _id);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill with all QuestDataSO")]

    public void CollectItemData()
    {

        string[] guids = AssetDatabase.FindAssets("t:ItemDataSO");

        allQuests = guids
            .Select(guids => AssetDatabase.LoadAssetAtPath<QuestDataSO>(AssetDatabase.GUIDToAssetPath(guids)))
            .Where(q => q != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
