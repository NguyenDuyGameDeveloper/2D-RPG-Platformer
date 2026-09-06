using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI description;

    public void ShowToolTip(string _description)
    {
        description.text = _description;

        gameObject.SetActive(true);
    }
    public void HideToolTip() => gameObject.SetActive(false);
}
