using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{

    [SerializeField] private FishSpawner[] fishSpawner;
    [SerializeField] private int[] scoreThreshold = new int[]
    {
        0,
        10,
        50,
        100
    };

    private readonly bool[] isWhiteDo = new bool[30];
    private readonly bool[] hasIncreasedSpawn =  new bool[30];

    private bool _isSceneChanging = false;

    private readonly Dictionary<string, (int fishValue, int scoreAdd)> fishValues = new Dictionary<string, (int fishValue, int scoreAdd)>
    {
        { "Fish_2", (2, 1) },
        { "Fish_10", (10, 5) },
        { "Fish_50", (50, 10) },
        { "Fish_150", (150, 15) },
        { "Fish_250", (250, 22) },
        { "Fish_375", (375, 25) },
        { "Fish_500", (500, 30) }
        // ����� �������� ������ ���� �� ��������
    };

    private int currentLevel = 1;

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
        if (_isSceneChanging)
        {
            return;  // ������������� ����������, ���� ����� ��������
        }

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


            if (currentScore >= scoreThreshold[1] && !hasIncreasedSpawn[1])
            {
                Level_1();
            }
            if (currentScore >= scoreThreshold[2] && !hasIncreasedSpawn[2])
            {
                Level_2();
            }
            if (currentScore >= scoreThreshold[3] && !hasIncreasedSpawn[3])
            {
                Level_3();
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


    private void Level_1()
    {
        // ����������� ���������� ����������� ��������
        fishSpawner[1].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[1] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_2()
    {
        fishSpawner[1].IncreaseSpawnRate(3f, 1);
        // ����������� ���������� ����������� ��������
        fishSpawner[2].IncreaseSpawnRate(1f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[2] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
    private void Level_3()
    {
        fishSpawner[2].IncreaseSpawnRate(2f, 2);
        // ����������� ���������� ����������� ��������
        fishSpawner[3].IncreaseSpawnRate(0.5f, 3);  // ��������� ����� ������ � ����������� ���������� ��������
        hasIncreasedSpawn[3] = true;  // ������ ����, ����� ��������� ����� ������ ���� ���
    }
}


//Time.timeScale = 0;
//AudioListener.pause = false;