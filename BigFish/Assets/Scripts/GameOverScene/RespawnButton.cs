using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButton : MonoBehaviour
{

    void OnMouseDown()
    {
        //����� ������� �� �������������� ������ 
        Yandex.Instance.RevardAdv();
        //����� ������� ���������� Yandex.Instance.AfterAdvAction();
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
