using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private UI ui;
    [SerializeField] private StatType statType;

    [SerializeField] private string statName;

    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if (statNameText != null)
        {
            statNameText.text = statName;
        }
    }
    private void Start()
    {
        UpdateStatValueUI();
        ui = GetComponentInParent<UI>();
    }
    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();

            if(statType == StatType.hearth)
                statValueText.text = playerStats.GetMaxHealthValue().ToString();

            if (statType == StatType.damage)
                statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();

            if (statType == StatType.critDamage)
                statValueText.text = (playerStats.critDamage.GetValue() + playerStats.strength.GetValue()).ToString();

            if (statType == StatType.critChange)
                statValueText.text = (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType == StatType.evasion)
                statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();

            if(statType == StatType.magicResistance)
                statValueText.text = (playerStats.magicResistance.GetValue() + (playerStats.intelligence.GetValue() * 3)).ToString();

            if (statType == StatType.evasion)
                statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType == StatType.armor)
                statValueText.text = (playerStats.armor.GetValue() + playerStats.vitality.GetValue()).ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideToolTip();
    }
}
