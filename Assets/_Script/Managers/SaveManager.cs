using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        if (dataHandler == null)
        {
            Debug.LogError("Failed to initialize dataHandler in SaveManager!");
            return;
        }

        saveManagers = FindAll_ISaveManagers();
        if (saveManagers == null || saveManagers.Count == 0)
        {
            Debug.LogError("No ISaveManagers found in SaveManager!");
        }

        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No Save Data found!");
            NewGame();
        }
        else
        {
            foreach (ISaveManager saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
        }
    }
    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<ISaveManager> FindAll_ISaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }
    public bool HasSaveData()
    {
        if (dataHandler.Load() != null)
            return true;

        return false;
    }
}
