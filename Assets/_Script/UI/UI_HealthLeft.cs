using TMPro;
using UnityEngine;

public class UI_HealthLeft : MonoBehaviour
{
    private TextMeshProUGUI healthLeft;
    private PlayerStats playerStats;

    private void Start()
    {
        healthLeft = GetComponent<TextMeshProUGUI>();
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    private void FixedUpdate()
    {
        UpdateHealthUI_Text();
    }
    private void UpdateHealthUI_Text()
    {
        int currentHealth = playerStats.currentHearth;
        int maxHealth = playerStats.maxHearth.GetValue();

        healthLeft.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        if (currentHealth < 0)
            healthLeft.text = 0 + " / " + maxHealth.ToString();
    }
}
