using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerInfo
{
    public int Speed;
    public int Gold;
    public int Level;

}

    public class Progress : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]

    private static extern void LoadExtern();

    [SerializeField] TextMeshProUGUI _playerInfoText;

    public PlayerInfo PlayerInfo;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //�������� ���������� ������ (����� �� ������ ����)
            //LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = PlayerInfo.Speed + "\n" + PlayerInfo.Gold + "\n" + PlayerInfo.Level;
    }

}
