using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainGame";
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_FadeScreen fadeScreen;
    private void Start()
    {
        if(SaveManager.instance.HasSaveData() == false)
            continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        //Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
