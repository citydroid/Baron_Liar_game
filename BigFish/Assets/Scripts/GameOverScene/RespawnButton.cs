using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    void OnMouseDown()
    {
        // �������� ��� ����� �� ������
        Debug.Log("Sprite button clicked!");

        // �� ������ ������� ����� �����, ����������� ������ � UI
        PerformAction();
    }

    void PerformAction()
    {
        // ShowAdv();
        SceneManager.LoadScene(1);
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

    void AdYandex(int value)
    {
        // ����������� � ��������� �����
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}
