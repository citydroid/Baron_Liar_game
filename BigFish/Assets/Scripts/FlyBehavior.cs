using Movers;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject deadPrefab;
    [SerializeField] private float destroyDelay = 1f;  // �����, ����� ������� ������������ ������ 2

    [SerializeField] private Transform cameraTransform;  // ������ �� ������
    [SerializeField] private float upperScreenThreshold = 0.75f; // ����� �� ������ ��� ������ ���� (75% ������)
    [SerializeField] private float lowerScreenThreshold = 0.25f; // ����� �� ������ ��� �������� ���� (25% ������)
    [SerializeField] private float cameraMoveSpeed = 2f;  // �������� �������� ����

    private Rigidbody2D _rb;
    private Transform _tr;
    private Animator playerAnimator;

    private bool playGame = true;
    private float cameraMinY; // ����������� �������� �� ��� Y ��� ������
    private float maxPlayerHeight; // ������������ ������ ������

    private Vector3 velocity = Vector3.zero; // ���������� ��� �������� �������� ������
    private float smoothTime = 1f; // �����, �� ������� ������ ��������� ������� �������
    private float predictionFactor = 10f; // ����������� ������������ �������

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
        // ������ ����������� ��������� ������ �� ������
        cameraMinY = cameraTransform.position.y;
        // �������� ������������ ������ �� GameManager
        maxPlayerHeight = gameManager.GetMaxPlayerHeight();
    }

    void Update()
    {
        if (playGame)
        {
            maxPlayerHeight = gameManager.GetMaxPlayerHeight();
            if (Input.GetMouseButtonDown(0) && _tr.position.y < maxPlayerHeight)
            {
                _rb.velocity = Vector2.up * _velocity;
            }

            if (_tr.position.y <= -1f)
            {
                _rb.velocity = Vector2.up * 1.5f;
            }
            // ��������� ��������� ������ ������������ ������
            HandleCameraMovement();
        }
    }
    private void HandleCameraMovement()
    {
        // ��������� ������� ������ �� ������� ��������� � ���������� ������ (0-1)
        Vector3 playerScreenPos = Camera.main.WorldToViewportPoint(_tr.position);

        // ������������ ������� ������� ������
        Vector3 targetPosition = cameraTransform.position;

        // ���� ����� ��������� ������� �������� ������
        if (playerScreenPos.y >= upperScreenThreshold)
        {
            // ������������� ���������� �������� ������, ������ ������ ���� ��� ���������
            float predictedYPosition = _tr.position.y + (cameraMoveSpeed * predictionFactor);
            targetPosition = new Vector3(cameraTransform.position.x, predictedYPosition, cameraTransform.position.z);
        }
        // ���� ����� ���������� ���� ������ �������� ������
        else if (playerScreenPos.y <= lowerScreenThreshold)
        {
            if (cameraTransform.position.y > cameraMinY)
            {
                // ������������� ���������� �������� ������ ����, �� �� �������� ������ ���� cameraMinY
                float predictedYPosition = _tr.position.y - (cameraMoveSpeed * predictionFactor);
                targetPosition = new Vector3(cameraTransform.position.x, Mathf.Max(predictedYPosition, cameraMinY), cameraTransform.position.z);
            }
        }

        // ������ ���������� ������ � ������������� �������
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int scoreAdd;
        int fishValue;

        if (playGame)
        {
            // ���������� GameManager ��� �������� � ���������� ���
            if (!gameManager.TryGetFishValue(collision.gameObject.tag, out fishValue, out scoreAdd))
            {
                return;// ���� ��� �� ����, ������� �� ������
            }

            if (Score.instance.GetScore() < fishValue)
            {
                playGame = false; // ��������� ����������� collision � ������� ���������
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
                // ��������� �� ������ (deadPrefab) ���������� ������������ �����
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
    }

    public void GameOverPlayer() // ���������� � ����� �������� PlayerDead
    {
        gameManager.GameOver();
    }
}
