using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    #region Info
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackHoleImage;
    [SerializeField] private Image potionImage;

    private SkillManager skills;

    [Header("Currency info")]
    [SerializeField] private TextMeshProUGUI currentGolds;
    [SerializeField] private float goldAmount;
    [SerializeField] private float inscreseRate;
    [SerializeField] private TextMeshProUGUI currentSkillPoints;
    #endregion

    private void Start()
    {
        if (playerStats != null)
            playerStats.onHearthChange += UpdateHearthUI;

        skills = SkillManager.instance;
    }
    private void Update()
    {
        UpdateCurrency();
        CheckInput();
    }
    private void IncreseRateOfGoldUI()
    {
        if (goldAmount < PlayerManager.instance.GetGold())
            goldAmount += Time.deltaTime * inscreseRate;
        else
            goldAmount = PlayerManager.instance.GetGold();
    }
    private void UpdateCurrency()
    {
        IncreseRateOfGoldUI();

        if (PlayerManager.instance.GetGold() == 0)
        {
            currentGolds.text = "0";
        }
        else
            currentGolds.text = ((int)goldAmount).ToString();
        currentSkillPoints.text = PlayerManager.instance.GetSkillPoint().ToString();
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && skills.dash.dashUnlocked)
            SetCoolDownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.Q) && skills.parry.parryUnlocked)
            SetCoolDownOf(parryImage);

        if (Input.GetKeyDown(KeyCode.F) && skills.crystal.crystalUnlocked)
            SetCoolDownOf(crystalImage);

        if (Input.GetKeyDown(KeyCode.Mouse1) && skills.sword.swordUnlocked)
            SetCoolDownOf(swordImage);

        if (Input.GetKeyDown(KeyCode.R) && skills.blackhole.blackHoleUnlocked)
            SetCoolDownOf(blackHoleImage);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Potion) != null)
            SetCoolDownOf(potionImage);

        CheckCooldownOf(dashImage, skills.dash.coolDown);
        CheckCooldownOf(parryImage, skills.parry.coolDown);
        CheckCooldownOf(crystalImage, skills.crystal.coolDown);
        CheckCooldownOf(swordImage, skills.sword.coolDown);
        CheckCooldownOf(blackHoleImage, skills.blackhole.coolDown);
        CheckCooldownOf(potionImage, Inventory.instance.potionCooldown);
    }
    private void UpdateHearthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHearth;
    }
    private void SetCoolDownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    private void CheckCooldownOf(Image _image, float _coolDown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
    }
}
