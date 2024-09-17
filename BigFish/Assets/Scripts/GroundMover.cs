using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundMover : MonoBehaviour
{
    public float _speed;
    private float targetX = -5f;
    void Start()
    {

    }

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
