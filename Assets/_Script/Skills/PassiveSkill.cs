using UnityEngine;
using UnityEngine.UI;

public class PassiveSkill : Skill
{
    #region Info
    #region Line 1 skill
    [Header("Line 1 skill")]
    [SerializeField] private UI_SkillTreeSlot_Passive unlockDamage_I_Button;
    public bool damage_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockArmor_I_Button;
    public bool armor_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockHealth_I_Button;
    public bool health_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockCritChange_I_Button;
    public bool critChange_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockCritDamage_I_Button;
    public bool critDamage_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockMagicResistance_I_Button;
    public bool magicResistance_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockStrengthButton;
    public bool strengthUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockAgilityButton;
    public bool agilityUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockVitalityButton;
    public bool vitalityUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockIntelligenceButton;
    public bool intelligenceUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockFireDamageButton;
    public bool fireDamageUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockIceDamageButton;
    public bool iceDamageUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockLightingDamageButton;
    public bool lightingDamageUnlocked { get; private set; }
    [Space]
    #endregion
    #region Line 2 skill
    [Header("Line 2 skill")]
    [SerializeField] private UI_SkillTreeSlot_Passive unlockUnited_I_Button;
    public bool united_I_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockDamage_II_Button;
    public bool damage_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockHealth_II_Button;
    public bool health_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockArmor_II_Button;
    public bool armor_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockCritChange_II_Button;
    public bool critChange_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockAddCrystalCapacityButton;
    public bool addCrystalCapacityUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockTheAgileHunterButton;
    public bool theAgileHunterUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockTheResilientScholarButton;
    public bool theResilientScholarUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockElementalExpertiseButton;
    public bool elementalExpertiseUnlocked { get; private set; }
    [Space]
    #endregion
    #region Line 3 skill
    [Header("Line 3 skill")]
    [SerializeField] private UI_SkillTreeSlot_Passive unlockEvasionButton;
    public bool evasionUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockHitHarderButton;
    public bool hitHarderUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockUnited_II_Button;
    public bool united_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockMagicResistance_II_Button;
    public bool magicResistance_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockCritDamage_II_Button;
    public bool critDamage_II_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockCriticalMasteryButton;
    public bool criticalMasteryUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockTheWiseMageButton;
    public bool theWiseMageUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockTheMightyWarriorButton;
    public bool theMightyWarriorUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockEternalFlameButton;
    public bool eternalFlameUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot_Passive unlockStorm_Is_WrathButton;
    public bool storm_Is_WrathUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot_Passive unlockGlacialBlizzardButton;
    public bool glacialBlizzardUnlocked { get; private set; }
    [Space]
    #endregion
    #region Line 4 skill
    [Header("Line 4 skill")]
    [SerializeField] private UI_SkillTreeSlot_Passive unlockMagicResistance_III_Button;
    public bool magicResistance_III_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockWarriorButton;
    public bool warriorUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockAssassinButton;
    public bool assassinUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockTankerButton;
    public bool tankerUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockMageButton;
    public bool mageUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockAstuteEvasionButton;
    public bool astuteEvasionUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockUnited_III_Button;
    public bool united_III_Unlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockUltimateUnityButton;
    public bool ultimateUnityUnlocked { get; private set; }

    [SerializeField] private UI_SkillTreeSlot_Passive unlockSupremeElementalistButton;
    public bool supremeElementalistUnlocked { get; private set; }

    #endregion
    #endregion

    protected override void Start()
    {
        base.Start();
        #region Button 1
        unlockDamage_I_Button.GetComponent<Button>().onClick.AddListener(UnlockDamage_I);
        unlockArmor_I_Button.GetComponent<Button>().onClick.AddListener(UnlockArmor_I);
        unlockHealth_I_Button.GetComponent<Button>().onClick.AddListener(UnlockHealth_I);
        unlockCritChange_I_Button.GetComponent<Button>().onClick.AddListener(UnlockCritChange_I);
        unlockCritDamage_I_Button.GetComponent<Button>().onClick.AddListener(UnlockCritDamage_I);
        unlockMagicResistance_I_Button.GetComponent<Button>().onClick.AddListener(UnlockMagicResistance_I);
        unlockStrengthButton.GetComponent<Button>().onClick.AddListener(UnlockStrength);
        unlockAgilityButton.GetComponent<Button>().onClick.AddListener(UnlockAgility);
        unlockVitalityButton.GetComponent<Button>().onClick.AddListener(UnlockVitality);
        unlockIntelligenceButton.GetComponent<Button>().onClick.AddListener(UnlockIntelligence);
        unlockFireDamageButton.GetComponent<Button>().onClick.AddListener(UnlockFireDamage);
        unlockIceDamageButton.GetComponent<Button>().onClick.AddListener(UnlockIceDamage);
        unlockLightingDamageButton.GetComponent<Button>().onClick.AddListener(UnlockLightingDamage);
        #endregion
        #region Button 2
        unlockUnited_I_Button.GetComponent<Button>().onClick.AddListener(UnlockUnited_I);
        unlockDamage_II_Button.GetComponent<Button>().onClick.AddListener(UnlockDamage_II);
        unlockHealth_II_Button.GetComponent<Button>().onClick.AddListener(UnlockHealth_II);
        unlockArmor_II_Button.GetComponent<Button>().onClick.AddListener(UnlockArmor_II);
        unlockCritChange_II_Button.GetComponent<Button>().onClick.AddListener(UnlockCritChange_II);
        unlockAddCrystalCapacityButton.GetComponent<Button>().onClick.AddListener(UnlockAddCrystalCapacity);
        unlockTheAgileHunterButton.GetComponent<Button>().onClick.AddListener(UnlockTheAgileHunter);
        unlockTheResilientScholarButton.GetComponent<Button>().onClick.AddListener(UnlockTheResilientScholar);
        unlockElementalExpertiseButton.GetComponent<Button>().onClick.AddListener(UnlockElementalExpertise);
        #endregion
        #region Button 3
        unlockEvasionButton.GetComponent<Button>().onClick.AddListener(UnlockEvasion);
        unlockHitHarderButton.GetComponent<Button>().onClick.AddListener(UnlockHitHarder);
        unlockUnited_II_Button.GetComponent<Button>().onClick.AddListener(UnlockUnited_II);
        unlockMagicResistance_II_Button.GetComponent<Button>().onClick.AddListener(UnlockMagicResistance_II);
        unlockCritDamage_II_Button.GetComponent<Button>().onClick.AddListener(UnlockCritDamage_II);
        unlockCriticalMasteryButton.GetComponent<Button>().onClick.AddListener(UnlockCriticalMastery);
        unlockTheWiseMageButton.GetComponent<Button>().onClick.AddListener(UnlockTheWiseMage);
        unlockTheMightyWarriorButton.GetComponent<Button>().onClick.AddListener(UnlockTheMightyWarrior);
        unlockEternalFlameButton.GetComponent<Button>().onClick.AddListener(UnlockEternalFlame);
        unlockStorm_Is_WrathButton.GetComponent<Button>().onClick.AddListener(UnlockStormIsWrath);
        unlockGlacialBlizzardButton.GetComponent<Button>().onClick.AddListener(UnlockGlacialBlizzard);
        #endregion
        #region Button 4
        unlockMagicResistance_III_Button.GetComponent<Button>().onClick.AddListener(UnlockMagicResistance_III);
        unlockWarriorButton.GetComponent<Button>().onClick.AddListener(UnlockWarrior);
        unlockAssassinButton.GetComponent<Button>().onClick.AddListener(UnlocAssassin);
        unlockTankerButton.GetComponent<Button>().onClick.AddListener(UnlockTanker);
        unlockMageButton.GetComponent<Button>().onClick.AddListener(UnlockMage);
        unlockAstuteEvasionButton.GetComponent<Button>().onClick.AddListener(UnlockAstuteEvasion);
        unlockUnited_III_Button.GetComponent<Button>().onClick.AddListener(UnlockUnited_III);
        unlockUltimateUnityButton.GetComponent<Button>().onClick.AddListener(UnlockUltimateUnity);
        unlockSupremeElementalistButton.GetComponent<Button>().onClick.AddListener(UnlocSupremeElementalist);
        #endregion
    }

    #region Unlock Line 1 Skill
    private void UnlockDamage_I()
    {
        if (unlockDamage_I_Button.unlocked && !damage_I_Unlocked)
        {
            player.stats.damage.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            damage_I_Unlocked = true;
        }
    }
    private void UnlockArmor_I()
    {
        if (unlockArmor_I_Button.unlocked && !armor_I_Unlocked)
        {
            player.stats.armor.AddModifier(2);
            Inventory.instance.UpdateStatsUI();
            armor_I_Unlocked = true;
        }
    }
    private void UnlockHealth_I()
    {
        if (unlockHealth_I_Button.unlocked && !health_I_Unlocked)
        {
            player.stats.maxHearth.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            health_I_Unlocked = true;
        }
    }
    private void UnlockCritChange_I()
    {
        if (unlockCritChange_I_Button.unlocked && !critChange_I_Unlocked)
        {
            player.stats.critChance.AddModifier(2);
            Inventory.instance.UpdateStatsUI();
            critChange_I_Unlocked = true;
        }
    }
    private void UnlockCritDamage_I()
    {
        if (unlockCritDamage_I_Button.unlocked && !critDamage_I_Unlocked)
        {
            player.stats.critDamage.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            critDamage_I_Unlocked = true;
        }
    }
    private void UnlockMagicResistance_I()
    {
        if (unlockMagicResistance_I_Button.unlocked && !magicResistance_I_Unlocked)
        {
            player.stats.magicResistance.AddModifier(2);
            Inventory.instance.UpdateStatsUI();
            magicResistance_I_Unlocked = true;
        }
    }
    private void UnlockStrength()
    {
        if (unlockStrengthButton.unlocked && !strengthUnlocked)
        {
            player.stats.strength.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            strengthUnlocked = true;
        }
    }
    private void UnlockAgility()
    {
        if (unlockAgilityButton.unlocked && !agilityUnlocked)
        {
            player.stats.agility.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            agilityUnlocked = true;
        }
    }
    private void UnlockVitality()
    {
        if (unlockVitalityButton.unlocked && !vitalityUnlocked)
        {
            player.stats.vitality.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            vitalityUnlocked = true;
        }
    }
    private void UnlockIntelligence()
    {
        if (unlockIntelligenceButton.unlocked && !intelligenceUnlocked)
        {
            player.stats.intelligence.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            intelligenceUnlocked = true;
        }
    }
    private void UnlockFireDamage()
    {
        if (unlockFireDamageButton.unlocked && !fireDamageUnlocked)
        {
            player.stats.fireDamage.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            fireDamageUnlocked = true;
        }
    }
    private void UnlockIceDamage()
    {
        if (unlockIceDamageButton.unlocked && !iceDamageUnlocked)
        {
            player.stats.iceDamage.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            iceDamageUnlocked = true;
        }
    }
    private void UnlockLightingDamage()
    {
        if (unlockLightingDamageButton.unlocked && !lightingDamageUnlocked)
        {
            player.stats.lightingDamage.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            lightingDamageUnlocked = true;
        }
    }
    #endregion
    #region Unlock Line 2 Skill
    private void UnlockUnited_I()
    {
        if (unlockUnited_I_Button.unlocked && !united_I_Unlocked)
        {
            player.stats.damage.AddModifier(1);
            player.stats.armor.AddModifier(1);
            player.stats.maxHearth.AddModifier(5);
            player.stats.magicResistance.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            united_I_Unlocked = true;
        }
    }
    private void UnlockDamage_II()
    {
        if (unlockDamage_II_Button.unlocked && !damage_II_Unlocked)
        {
            player.stats.damage.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            damage_II_Unlocked = true;
        }
    }
    private void UnlockHealth_II()
    {
        if (unlockHealth_II_Button.unlocked && !health_II_Unlocked)
        {
            player.stats.maxHearth.AddModifier(20);
            Inventory.instance.UpdateStatsUI();
            health_II_Unlocked = true;
        }
    }
    private void UnlockArmor_II()
    {
        if (unlockArmor_II_Button.unlocked && !armor_II_Unlocked)
        {
            player.stats.armor.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            armor_II_Unlocked = true;
        }
    }
    private void UnlockCritChange_II()
    {
        if (unlockCritChange_II_Button.unlocked && !critChange_II_Unlocked)
        {
            player.stats.critChance.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            critChange_II_Unlocked = true;
        }
    }
    private void UnlockAddCrystalCapacity()
    {
        if (unlockAddCrystalCapacityButton.unlocked && !addCrystalCapacityUnlocked)
        {
            player.skill.crystal.amountOfStacks += 1;
            addCrystalCapacityUnlocked = true;
        }
    }
    private void UnlockTheAgileHunter()
    {
        if (unlockTheAgileHunterButton.unlocked && !theAgileHunterUnlocked)
        {
            player.stats.strength.AddModifier(5);
            player.stats.agility.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            theAgileHunterUnlocked = true;
        }
    }
    private void UnlockTheResilientScholar()
    {
        if (unlockTheResilientScholarButton.unlocked && !theResilientScholarUnlocked)
        {
            player.stats.intelligence.AddModifier(3);
            player.stats.vitality.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            theResilientScholarUnlocked = true;
        }
    }
    private void UnlockElementalExpertise()
    {
        if (unlockElementalExpertiseButton.unlocked && !elementalExpertiseUnlocked)
        {
            player.stats.fireDamage.AddModifier(3);
            player.stats.iceDamage.AddModifier(3);
            player.stats.lightingDamage.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            elementalExpertiseUnlocked = true;
        }
    }
    #endregion
    #region Unlock Line 3 Skill
    private void UnlockEvasion()
    {
        if (unlockEvasionButton.unlocked && !evasionUnlocked)
        {
            player.stats.evasion.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            evasionUnlocked = true;
        }
    }
    private void UnlockHitHarder()
    {
        if (unlockHitHarderButton.unlocked && !hitHarderUnlocked)
        {
            player.stats.damage.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            hitHarderUnlocked = true;
        }
    }   
    private void UnlockUnited_II()
    {
        if (unlockUnited_II_Button.unlocked && !united_II_Unlocked)
        {
            player.stats.damage.AddModifier(3);
            player.stats.armor.AddModifier(3);
            player.stats.magicResistance.AddModifier(3);
            player.stats.maxHearth.AddModifier(20);
            Inventory.instance.UpdateStatsUI();
            united_II_Unlocked = true;
        }
    }
    private void UnlockMagicResistance_II()
    {
        if (unlockMagicResistance_II_Button.unlocked && !magicResistance_II_Unlocked)
        {
            player.stats.magicResistance.AddModifier(4);
            Inventory.instance.UpdateStatsUI();
            magicResistance_II_Unlocked = true;
        }
    }
    private void UnlockCritDamage_II()
    {
        if (unlockCritDamage_II_Button.unlocked && !critDamage_II_Unlocked)
        {
            player.stats.critDamage.AddModifier(15);
            Inventory.instance.UpdateStatsUI();
            critDamage_II_Unlocked = true;
        }
    }
    private void UnlockCriticalMastery()
    {
        if (unlockCriticalMasteryButton.unlocked && !criticalMasteryUnlocked)
        {
            player.stats.critChance.AddModifier(5);
            player.stats.critDamage.AddModifier(30);
            Inventory.instance.UpdateStatsUI();
            criticalMasteryUnlocked = true;
        }
    }
    private void UnlockTheWiseMage()
    {
        if (unlockTheWiseMageButton.unlocked && !theWiseMageUnlocked)
        {
            player.stats.intelligence.AddModifier(5);
            player.stats.agility.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            theWiseMageUnlocked = true;
        }
    }
    private void UnlockTheMightyWarrior()
    {
        if (unlockTheMightyWarriorButton.unlocked && !theMightyWarriorUnlocked)
        { 
            player.stats.strength.AddModifier(8);
            player.stats.vitality.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            theMightyWarriorUnlocked = true;
        }
    }
    private void UnlockEternalFlame()
    {
        if (unlockEternalFlameButton.unlocked && !eternalFlameUnlocked)
        {
            player.stats.fireDamage.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            eternalFlameUnlocked = true;
        }
    }
    private void UnlockStormIsWrath()
    {
        if (unlockStorm_Is_WrathButton.unlocked && !storm_Is_WrathUnlocked)
        {
            player.stats.iceDamage.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            storm_Is_WrathUnlocked = true;
        }
    }
    private void UnlockGlacialBlizzard()
    {
        if (unlockGlacialBlizzardButton.unlocked && !glacialBlizzardUnlocked)
        {
            player.stats.lightingDamage.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            glacialBlizzardUnlocked = true;
        }
    }
    #endregion  
    #region Unlock Line 4 Skill
    private void UnlockMagicResistance_III()
    {
        if (unlockMagicResistance_III_Button.unlocked && !magicResistance_III_Unlocked)
        {
            player.stats.magicResistance.AddModifier(6);
            Inventory.instance.UpdateStatsUI();
            magicResistance_III_Unlocked = true;
        }
    }
    private void UnlockWarrior()
    {
        if (unlockWarriorButton.unlocked && !warriorUnlocked)
        {
            player.stats.strength.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            warriorUnlocked = true;
        }
    }
    private void UnlocAssassin()
    {
        if (unlockAssassinButton.unlocked && !assassinUnlocked)
        {
            player.stats.agility.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            assassinUnlocked = true;
        }
    }
    private void UnlockTanker()
    {
        if (unlockTankerButton.unlocked && !tankerUnlocked)
        {
            player.stats.vitality.AddModifier(5);
            Inventory.instance.UpdateStatsUI();
            tankerUnlocked = true;
        }
    }
    private void UnlockMage()
    {
        if (unlockMageButton.unlocked && !mageUnlocked)
        {
            player.stats.intelligence.AddModifier(3);
            Inventory.instance.UpdateStatsUI();
            mageUnlocked = true;
        }
    }
    private void UnlockAstuteEvasion()
    {
        if (unlockAstuteEvasionButton.unlocked && !astuteEvasionUnlocked)
        {
            player.stats.evasion.AddModifier(8);
            Inventory.instance.UpdateStatsUI();
            astuteEvasionUnlocked = true;
        }
    }
    private void UnlockUnited_III()
    {
        if (unlockUnited_III_Button.unlocked && !united_III_Unlocked)
        {
            player.stats.damage.AddModifier(5);
            player.stats.magicResistance.AddModifier(5);
            player.stats.armor.AddModifier(5);
            player.stats.maxHearth.AddModifier(30);
            Inventory.instance.UpdateStatsUI();
            united_III_Unlocked = true;
        }
    }
    private void UnlockUltimateUnity()
    {
        if (unlockUltimateUnityButton.unlocked && !ultimateUnityUnlocked)
        {
            player.stats.damage.AddModifier(10);
            player.stats.magicResistance.AddModifier(10);
            player.stats.armor.AddModifier(10);
            player.stats.maxHearth.AddModifier(50);
            Inventory.instance.UpdateStatsUI();
            ultimateUnityUnlocked = true;
        }
    }
    private void UnlocSupremeElementalist()
    {
        if (unlockSupremeElementalistButton.unlocked && !supremeElementalistUnlocked)
        {
            player.stats.fireDamage.AddModifier(10);
            player.stats.iceDamage.AddModifier(10);
            player.stats.lightingDamage.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            supremeElementalistUnlocked = true;
        }
    }
    #endregion
}
