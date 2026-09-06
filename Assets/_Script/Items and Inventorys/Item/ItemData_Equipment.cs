using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Potion
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemDataSO
{
    public EquipmentType equipmentType;

    private int descriptionLength;

    [Header("Unique effects")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;

    #region Major stats
    [Header("Major stats")]
    public int strength;         //STR: 1 pt = 1 damage,        1 pt = .1% crit damage
    public int agility;          //AGI: 1 pt = .1% evasion,     1 pt = .1% crit change
    public int intelligence;     //INT: 1 pt = 1 magic damage,  1 pt = 3 magic resistance
    public int vitality;         //VIT: 1 pt = 5 HP,            1 pt = 1 physical resistance
    #endregion
    #region Offensive stats
    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critDamage;       //Default value: 150%
    #endregion
    #region Defensive stats
    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int magicResistance;
    public int evasion;
    #endregion
    #region Magic stats
    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;
    #endregion

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critDamage.AddModifier(critDamage);

        playerStats.maxHearth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.magicResistance.AddModifier(magicResistance);
        playerStats.evasion.AddModifier(evasion);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }
    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critDamage.RemoveModifier(critDamage);

        playerStats.maxHearth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.magicResistance.RemoveModifier(magicResistance);
        playerStats.evasion.RemoveModifier(evasion);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }
    public override string GetDescription()
    {
        sb.Length = 0;

        descriptionLength = 0;

        AddItemDescription(strength, "Strength");
        AddItemDescription(agility, "Agility");
        AddItemDescription(intelligence, "Intelligence");
        AddItemDescription(vitality, "Vitality");

        AddItemDescription(damage, "Damage");
        AddItemDescription(critChance, "Crit chance");
        AddItemDescription(critDamage, "Crit damage");

        AddItemDescription(health, "Health");
        AddItemDescription(evasion, "Evasion");
        AddItemDescription(armor, "Armor");
        AddItemDescription(magicResistance, "Magic Resis");

        AddItemDescription(fireDamage, "Fire dmg");
        AddItemDescription(iceDamage, "Ice dmg");
        AddItemDescription(lightingDamage, "Lighting dmg");

        for (int i = 0; i < itemEffects.Length; i++)
        {
            if (itemEffects[i].effectDescription.Length > 0)
            {
                sb.AppendLine();

                if (equipmentType == EquipmentType.Potion)
                {
                    sb.Append("");
                    sb.AppendLine("Effect: " + itemEffects[i].effectDescription);
                }
                else
                {
                    sb.Append("");
                    sb.AppendLine("Unique: " + itemEffects[i].effectDescription);
                }

                descriptionLength++;
            }
        }

        if(descriptionLength < 3)
        {
            for (int i = 0; i < 3 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }
    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);

            descriptionLength++;
        }
    }
}
