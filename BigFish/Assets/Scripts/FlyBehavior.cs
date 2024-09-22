using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject deadPrefab;
    [SerializeField] private float destroyDelay = 1f;  // �����, ����� ������� ������������ ������ 2
    [SerializeField] private int currentLevel = 1; // ������� �������
    [SerializeField] private int nextFishValue; // ��������� �������� fishValue ��� ������


    private Rigidbody2D _rb;
    private Transform _tr;
    private Animator playerAnimator;
    private FishCollider fishCollider;

    private bool playGame = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
        fishCollider = GetComponent<FishCollider>();  // �������� ������ �� ��������� FishCollider

        // ������������� ���������� �������� fishValue ��� ������� ������
  //     nextFishValue = FishCollider.GetFishValueForLevel(currentLevel);
    }

    void Update()
    {
        if (playGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = Vector2.up * _velocity;
            }

            if (_tr.position.y <= -1f)
            {
                _rb.velocity = Vector2.up * 1.5f;
            }

            // ���������, ���� ���� ������ �������� ���������� fishValue ��� �������� ������
            if (Score.instance.GetScore() >= nextFishValue)
            {
                // �������� ������� "Red" ��� ���� ��� �������� ������
                HideRedObjectsIfScoreMet(nextFishValue);

                // ��������� �� ��������� �������
                currentLevel++;
                // ��������� ��������� �������� fishValue ��� ������ ������
          //      nextFishValue = FishCollider.GetFishValueForLevel(currentLevel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int scoreAdd;
        int fishValue;

        // ���������� ����� ����� FishCollider ��� �������� ���
        if (!fishCollider.TryGetFishValue(collision.gameObject.tag, out fishValue, out scoreAdd))
        {
            // ���� ��� �� ����, ������� �� ������
            return;
        }

        if (Score.instance.GetScore() < fishValue)
        {
            playGame = false;
            playerAnimator.Play("PlayerDead");
        }
        else
        {
            playerAnimator.Play("PlayerEating");
            Score.instance.UpdateScore(scoreAdd);

            // ���������� ������������� ������
            Destroy(collision.gameObject);

            // ������� "�������" ������ (deadPrefab) �� ����� ������������ ����
            Vector3 spawnPosition = collision.gameObject.transform.position;
            GameObject newObject2 = Instantiate(deadPrefab, spawnPosition, Quaternion.identity);

            UpdateDeadText(newObject2, scoreAdd);

            // ���������� ����� ������ ����� ������������ �����
            Destroy(newObject2, destroyDelay);
        }
    }

    private void UpdateDeadText(GameObject deadObject, int scoreAdd)
    {
        DeadScore deadScore = deadObject.GetComponent<DeadScore>();

        if (deadScore != null)
        {
            deadScore.ScoreValue(scoreAdd);
        }
        else
        {
            Debug.LogWarning("��������� DeadScore �� ������ �� �������: " + deadObject.name);
        }
    }

    public void GameOverPlayer()
    {
        gameManager.GameOver();
    }
    private void HideRedObjectsIfScoreMet(int fishValue)
    {
        // ������ ��� ������� �������� "Red" (��� �� �����, ��� ��� ����)
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
}
