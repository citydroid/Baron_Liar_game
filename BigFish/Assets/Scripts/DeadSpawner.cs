using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpawner : MonoBehaviour
{
    public GameObject _fish; // ������ ����
    public float destroyDelay = 1f;  // ����� �� ����������� �������

    // ���� ����� ���������� ��� �������� ������� � ��� �����������
    public void SpawnAndDestroy(Vector3 spawnPosition)
    {
        // ������ ������ 2 (��������, ����)
        GameObject _newFish = Instantiate(_fish, spawnPosition, Quaternion.identity);

        // ���� ����� ��������� �������� �� �������
        Animator animator = _newFish.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("FishAnimation"); // �������� ����� ��������
        }

        // ���������� ������ ����� destroyDelay ������
        Destroy(_newFish, destroyDelay);
    }
}
