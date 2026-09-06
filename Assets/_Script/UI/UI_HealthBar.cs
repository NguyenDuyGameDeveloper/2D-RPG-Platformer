using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats myStats => GetComponentInParent<CharacterStats>();
    private RectTransform myTransform;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        myTransform = GetComponent<RectTransform>();

        UpdateHearthUI();
    }
    private void UpdateHearthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHearth;
    }
    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHearthChange += UpdateHearthUI;
    }
    private void OnDisable()
    {
        if(entity != null)
            entity.onFlipped -= FlipUI;

        if(myStats != null)
            myStats.onHearthChange -= UpdateHearthUI;
    }
    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}
