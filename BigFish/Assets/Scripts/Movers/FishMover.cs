using UnityEngine;

namespace Movers
{
    public class FishMover : MonoBehaviour
    {
        [SerializeField] private int fishNumber = 0;

        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField] private float maxSpeed = 1.5f;
        private float _horizontalSpeed;

        [SerializeField] private float minVertSpeed = 0f;
        [SerializeField] private float maxVertSpeed = 0f;
        private float _verticalSpeed;

        private readonly float targetX = -5f;
        private float _maxVertical;
        private float _minVertical = -1f;  // ����������� ���������� ������
        private float depthCoeff = 0.0002f;

        private bool playGame = true;

        void Start()
        {
            _horizontalSpeed = Random.Range(minSpeed, maxSpeed);
            _verticalSpeed = Random.Range(minVertSpeed, maxVertSpeed);
        }

        void Update()
        {
            if (playGame)
            {
                _maxVertical += depthCoeff;
                // ���������, �������� �� ���� ������� �������
                if (transform.position.y >= _maxVertical)
                {
                    transform.position = new Vector3(transform.position.x, _maxVertical, transform.position.z);
                    _verticalSpeed = -Mathf.Abs(_verticalSpeed); // �������� ���� ����� ���������� ������� �������
                }
                // ���������, �������� �� ���� ������ �������
                else if (transform.position.y <= _minVertical)
                {
                    transform.position = new Vector3(transform.position.x, _minVertical, transform.position.z);
                    _verticalSpeed = Mathf.Abs(_verticalSpeed); // �������� ����� ����� ���������� ������ �������
                }

                // ������ ��� ���������� ��� �� �� �������
                if (fishNumber == 10)
                {
                    _verticalSpeed = 0; // ��� ���� � ������� 10 ������������ �������� ���������������
                }
                else if (fishNumber == 50 || fishNumber == 150)
                {
                    // ���� � �������� 50 � 150 ���������� �������� �����-����
                    if (transform.position.y >= _maxVertical || transform.position.y <= _minVertical)
                    {
                        _verticalSpeed = -_verticalSpeed;  // ������ ����������� ��������
                    }
                }

                // ��������� ������� ���� � ������ �������������� � ������������ ��������
                transform.position += new Vector3(-_horizontalSpeed, _verticalSpeed, 0) * Time.deltaTime;

                // ���������� ������, ���� �� ������� �� ������� ������
                if (transform.position.x <= targetX)
                {
                    Destroy(gameObject);
                }
            }
        }

        // ��������� ����� ��� ������� ������������ ������
        public void SetMaxVertical(float maxValue)
        {
            _maxVertical = maxValue - 0.2f;  // � ��������� �������� �� �������� ������
        }
        public void SetDepth(float maxVertical, float depthValue)
        {
            _maxVertical = maxVertical - 0.1f;
            depthCoeff = depthValue;  // � ��������� �������� �� �������� ������
        }
        // ��������� ����� ��� ��������� ������������ ��������
        public void SetVerticalSpeed(float verticalSpeed)
        {
            _verticalSpeed = verticalSpeed;
        }

        // ��������� ����� ��� ��������� �������������� ��������
        public void SetHorizontalSpeed(float horizontalSpeed)
        {
            _horizontalSpeed = horizontalSpeed;
        }

        // ��������� ����� ��� ���������� �������������� �������� �� ��������� ��������
        public void IncreaseHorizontalSpeed(float increaseAmount)
        {
            _horizontalSpeed += increaseAmount;
        }
    }
}
