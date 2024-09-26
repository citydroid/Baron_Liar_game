using UnityEngine;

namespace Movers
{
    public class WaveMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float targetX = -5f;

        void Update()
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
            // ���������� ������, ���� �� ������� �� ������� ������
            if (transform.position.x <= targetX)
            {
                Destroy(gameObject);
            }
        }
    }
}