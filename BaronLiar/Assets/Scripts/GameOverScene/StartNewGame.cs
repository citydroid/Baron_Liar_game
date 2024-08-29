using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
    void OnMouseDown()
    {
        // �������� ��� ����� �� ������
        Debug.Log("Sprite button clicked!");

        // �� ������ ������� ����� �����, ����������� ������ � UI
        PerformAction();
    }

    void PerformAction()
    {
        SceneManager.LoadScene(0);
    }

    void OnMouseEnter()
    {
        // ��������� ����� ������� ��� ��������� �������
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void OnMouseExit()
    {
        // ����������� � ��������� �����
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
