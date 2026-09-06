using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] string closestCheckPointId;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        if (checkPoints == null || checkPoints.Length == 0)
        {
            Debug.LogError("CheckPoints array is not initialized or empty in GameManager!");
        }
    }
    private void Reset()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }
    public void RestartScreen()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint == null)
                {
                    Debug.LogError("CheckPoint is null in GameManager!");
                    continue;
                }
                if (checkPoint.id == pair.Key && pair.Value == true)
                    checkPoint.ActivateCheckPoint();
            }
        }
        closestCheckPointId = _data.closestCheckPointId;
        Invoke(nameof(PlacePlayerAtClosestCheckPoint), .1f);
    }

    private void PlacePlayerAtClosestCheckPoint()
    {
        if (PlayerManager.instance == null || PlayerManager.instance.player == null)
        {
            Debug.LogError("PlayerManager or Player is null!");
            return;
        }

        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (closestCheckPointId == checkPoint.id && checkPoint.activationStatus == true)
                PlayerManager.instance.player.transform.position = checkPoint.transform.position;
        }
    }
    public void SaveData(ref GameData _data)
    {
        _data.closestCheckPointId = FindClosetCheckPoint().id;
        _data.checkPoints.Clear();

        foreach (CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.id, checkPoint.activationStatus);
        }
    }
    private CheckPoint FindClosetCheckPoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckPoint = null;

        foreach (var checkPoint in checkPoints)
        {
            float distanceToCheckPoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkPoint.transform.position);

            if (distanceToCheckPoint < closestDistance)
            {
                closestDistance = distanceToCheckPoint;
                closestCheckPoint = checkPoint;
            }
        }
        return closestCheckPoint;
    }
    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
