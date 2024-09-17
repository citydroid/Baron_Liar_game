using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSceneButton : MonoBehaviour
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
        ///TO DO �������� ��� ����. ���������!!
        Score.instance.Obnulit();
        /////////////////////////////////////
        ///
        Progress.Instance.Save();
        SceneManager.LoadScene(3);
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
