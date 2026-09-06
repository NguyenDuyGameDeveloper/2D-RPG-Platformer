using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour,ISaveManager
{
    #region Info
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreenUI;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [Space]
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject inGameUI;
    [Space]
    [SerializeField] private GameObject activeSkillPanelUI;
    [Space]
    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;
    public UI_CraftWindow craftWindow;
    public bool alternativeInput { get; private set; }
    private PlayerInputSet input;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;
    #endregion

    private void Awake()
    {
        SwitchTo(skillTreeUI);
        SwitchTo(activeSkillPanelUI);
        fadeScreenUI.gameObject.SetActive(true);
    }
    private void Start()
    {
        SwitchTo(inGameUI);
        SwitchOffToolTipByDefault();
    }
    private void Update()
    {
        Input();
    }
    public void SetupControlUI(PlayerInputSet _inputSet)
    {
        input = _inputSet;

    }
    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
            SwitchWithKeyTo(characterUI);

        if (UnityEngine.Input.GetKeyDown(KeyCode.K))
            SwitchWithKeyTo(skillTreeUI);

        if (UnityEngine.Input.GetKeyDown(KeyCode.B))
            SwitchWithKeyTo(craftUI);

        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            SwitchWithKeyTo(shopUI);

        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
            SwitchWithKeyTo(optionUI);
    }
    private void SwitchOffToolTipByDefault()
    {
        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        skillToolTip.gameObject.SetActive(false);
    }   
    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool FadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;
            if (!FadeScreen)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySFX(6,null);
            _menu.SetActive(true);
        }

        if(GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
    }
    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }
    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return; 
        }

        SwitchTo(inGameUI);
    }
    public void SwitchOnEndScreen()
    {
        fadeScreenUI.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }
    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }
    public void RestartGameButton() => GameManager.instance.RestartScreen();
    public void LoadData(GameData _data)
    {
        foreach(KeyValuePair<string,float> pair in _data.volumeSettings)
        {
            foreach(UI_VolumeSlider item in volumeSettings)
            {
                if(item.parameter ==  pair.Key)
                    item.LoadSlider(pair.Value);
            }
        }
    }
    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }
}
