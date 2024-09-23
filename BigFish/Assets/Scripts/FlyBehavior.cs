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


    private Rigidbody2D _rb;
    private Transform _tr;
    private Animator playerAnimator;

    private bool playGame = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int scoreAdd;
        int fishValue;

        if (playGame)
        {
            // ���������� ����� ����� FishCollider ��� �������� ���
            if (!gameManager.TryGetFishValue(collision.gameObject.tag, out fishValue, out scoreAdd))
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

    public void GameOverPlayer() // ���������� � ����� �������� PlayerDead
    {
        gameManager.GameOver();
    }
}
