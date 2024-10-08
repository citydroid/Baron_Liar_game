using Movers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseObject;
    [SerializeField] private bool isTerra1 = false;

    private float darkerColor = 1f;  // ����� ���� ��� �������
    public GameObject terra_1;
    public GameObject terra_2;
    public float MaxTime = 1.0f;

    private float height = 0;
    private float _timer = 0;
    private GameObject _terra;
    private int terra = 0;

    void Start()
    {
        if (isTerra1)
        {
            _terra = terra_1;
        }
        else
        {
            _terra = terra_2;
        }

        Spawner();
    }

    void Update()
    {
        if (_timer > MaxTime)
        {
         //  ��� ���������� ���������
            terra = Random.Range(1, 10);
            if (terra <= 5) { _terra = terra_2; }
            else if (terra > 5) { _terra = terra_1; }
     
            Spawner();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void Spawner()
    {
        Vector3 position = transform.position + new Vector3(0, Random.Range(-height, height), 0);

        GameObject _newFish = Instantiate(_terra, position, Quaternion.identity);
        _newFish.transform.SetParent(baseObject.transform, false);
       // ChangeObjectColor(_newFish, darkerColor);
    }
    /*
    private void ChangeObjectColor(GameObject existingObject, float grayLevel)
    {
        TerraMover mover = existingObject.GetComponent<TerraMover>();
        if (mover != null)
        {
            mover.SetGrayScale(grayLevel);  // ������������� ����� ������� ������
        }
    }
    public void ChangeGrayLevel(float depthCoeff)
    {
        darkerColor -= depthCoeff;
    }
    */
}
