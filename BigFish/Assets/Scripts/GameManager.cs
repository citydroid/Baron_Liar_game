using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{

    [SerializeField] private FishSpawner[] fishSpawner;
    [SerializeField] private int[] scoreThreshold;

    private readonly bool[] isWhiteDo = new bool[30];
    private readonly bool[] hasIncreasedSpawn =  new bool[30];

    private bool _isSceneChanging = false;
    private int currentLevel = 1;

    private readonly Dictionary<string, (int fishValue, int scoreAdd)> fishValues = new Dictionary<string, (int fishValue, int scoreAdd)>
    {
        { "Fish_2", (2, 1) },
        { "Fish_10", (10, 5) },
        { "Fish_50", (50, 10) },
        { "Fish_150", (150, 15) },
        { "Fish_250", (250, 22) },
        { "Fish_375", (375, 25) },
        { "Fish_500", (500, 30) }
    };

    // ������ �������� �������
    private readonly List<LevelSettings> levelSettings = new List<LevelSettings>
    {
        // ��������� ��� ������ 0
        new LevelSettings(0, new[] { (1, 0.5f, 3) }),
        // ��������� ��� ������ 1
        new LevelSettings(10, new[] { (1, 1f, 1), (2, 1f, 1), (3, 1f, 3) }),
        // ��������� ��� ������ 2
        new LevelSettings(50, new[] { (1, 3f, 1), (2, 1f, 3), (5, 1f, 3) }),
        // ��������� ��� ������ 3
        new LevelSettings(100, new[] { (2, 2f, 2), (3, 0.5f, 3) })
    };
    public void Start()
    {
        Time.timeScale = 1.0f;
        transform.parent = null;
        // ������������� �� ������� �������� ����� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���������� ���� ��� �������� ����� �����
        _isSceneChanging = false;
    }
    private void Update()
    {
        if (_isSceneChanging) return; 

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int currentScore = Score.instance.GetScore();

            // ���������, ���� ���� ������ �������� ���������� fishValue ��� �������� ������
            if (currentScore >= scoreThreshold[currentLevel])
            {
                // �������� ������� "Red" ��� ���� ��� �������� ������
                HideRedObjectsIfScoreMet(scoreThreshold[currentLevel]);
                // ��������� �� ��������� �������
                currentLevel++;
                Progress.Instance.PlayerInfo.Level = currentLevel;
                // �������� ���� "isWhite" � �������� ��� �������� ������
                fishSpawner[currentLevel].IsWhiteSwitch();
            }

            //ProcessLevelProgress(currentScore);

            if (currentScore >= scoreThreshold[0] && !hasIncreasedSpawn[0])
            {
                Level_0(); Debug.Log("0 " + currentScore + "  : " + scoreThreshold[0]);
            }
            if (currentScore >= scoreThreshold[1] && !hasIncreasedSpawn[1])
            {
                Level_1(); Debug.Log("1 " + currentScore + "  : " + scoreThreshold[1]);
            }
            if (currentScore >= scoreThreshold[2] && !hasIncreasedSpawn[2])
            {
                Level_2(); Debug.Log("2 " + currentScore + "  : " + scoreThreshold[2]);
            }
            if (currentScore >= scoreThreshold[3] && !hasIncreasedSpawn[3])
            {
                Level_3(); Debug.Log("3 " + currentScore + "  : " + scoreThreshold[3]);
            }
            if (currentScore >= scoreThreshold[4] && !hasIncreasedSpawn[4])
            {
                Level_4(); Debug.Log("4 " + currentScore + "  : " + scoreThreshold[4]);
            }
            if (currentScore >= scoreThreshold[5] && !hasIncreasedSpawn[5])
            {
                Level_5(); Debug.Log("5 " + currentScore + "  : " + scoreThreshold[5]);
            }
            if (currentScore >= scoreThreshold[6] && !hasIncreasedSpawn[6])
            {
                Level_6(); Debug.Log("6 " + currentScore + "  : " + scoreThreshold[6]);
            }
        }
    }
    private void HideRedObjectsIfScoreMet(int fishValue)
    {
        // ������ ��� ������� �������� "Red"
        GameObject[] allFishObjects = GameObject.FindGameObjectsWithTag($"Fish_{fishValue}");

        foreach (GameObject fish in allFishObjects)
        {
            if (Score.instance.GetScore() >= fishValue)
            {
                Transform redTransform = fish.transform.Find("Red");
                if (redTransform != null)
                {
                    GameObject redObject = redTransform.gameObject;
                    redObject.SetActive(false);
                }
            }
        }
    }
    private void ProcessLevelProgress(int currentScore)
    {
        if (currentLevel >= levelSettings.Count) return;

        var settings = levelSettings[currentLevel];

        if (currentScore >= settings.RequiredScore && !hasIncreasedSpawn[currentLevel])
        {
            ApplyLevelSettings(settings);
            hasIncreasedSpawn[currentLevel] = true;
        }
    }

    private void ApplyLevelSettings(LevelSettings settings)
    {
        foreach (var (spawnerIndex, spawnRate, spawnAmount) in settings.SpawnActions)
        {
            fishSpawner[spawnerIndex].IncreaseSpawnRate(spawnRate, spawnAmount);
        }
    }
    public void GameOver()
    {
        Score.instance.UpdateGold();
        SceneManager.LoadScene("2_GameOverScene");
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }


    public bool TryGetFishValue(string fishTag, out int fishValue, out int scoreAdd)
    {
        if (fishValues.TryGetValue(fishTag, out var values))
        {
            fishValue = values.fishValue;
            scoreAdd = values.scoreAdd;
            return true;
        }

        fishValue = 0;
        scoreAdd = 0;
        return false;
    }

    private void Level_0()
    {
        // ����������� ���������� ����������� ��������
        fishSpawner[1].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[0] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_1()
    {
        // ����������� ���������� ����������� ��������
        fishSpawner[1].IncreaseSpawnRate(1f, 1);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[1] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
        fishSpawner[2].IncreaseSpawnRate(1f, 1);
        fishSpawner[2].IncreaseSpawnHight(1f);
        fishSpawner[3].IncreaseSpawnRate(1f, 3);
        fishSpawner[4].IncreaseSpawnRate(1f, 1);
    }
    private void Level_2()
    {
        fishSpawner[1].IncreaseSpawnRate(3f, 1);
        // ����������� ���������� ����������� ��������
        fishSpawner[2].IncreaseSpawnRate(1f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[2] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
        fishSpawner[3].IncreaseSpawnRate(1f, 1);
        fishSpawner[5].IncreaseSpawnRate(1f, 3);
        fishSpawner[7].IncreaseSpawnRate(5f, 1);
    }
    private void Level_3()
    {
        fishSpawner[2].IncreaseSpawnRate(2f, 2);
        // ����������� ���������� ����������� ��������
        fishSpawner[3].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[3] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_4()
    {
        fishSpawner[3].IncreaseSpawnRate(2f, 2);
        // ����������� ���������� ����������� ��������
        fishSpawner[4].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[4] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_5()
    {
        fishSpawner[4].IncreaseSpawnRate(2f, 2);
        // ����������� ���������� ����������� ��������
        fishSpawner[5].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[5] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_6()
    {
        fishSpawner[5].IncreaseSpawnRate(2f, 2);
        // ����������� ���������� ����������� ��������
        fishSpawner[6].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[6] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
}

// ��������� ��� �������� �������� �� ������
// ������������ ����� ��� �������� ������ � ��������
[System.Serializable]
public class LevelSettings
{
    public int RequiredScore; // ����� ����� ��� �������� �� �������
    public List<(int spawnerIndex, float spawnRate, int spawnAmount)> SpawnActions; // �������� ��� ���������� ������

    public LevelSettings(int requiredScore, params (int spawnerIndex, float spawnRate, int spawnAmount)[] actions)
    {
        RequiredScore = requiredScore;
        SpawnActions = new List<(int spawnerIndex, float spawnRate, int spawnAmount)>(actions);
    }
}
//Time.timeScale = 0;
//AudioListener.pause = false;