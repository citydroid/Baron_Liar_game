using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RewardedAdv();
    void OnMouseDown()
    {
        //����� ������� �� �������������� ������ 
        RewardedAdv();
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
