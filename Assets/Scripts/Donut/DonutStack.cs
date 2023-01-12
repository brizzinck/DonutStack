using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutStack : MonoBehaviour
{
    [SerializeField] private Donut _donut;
    [SerializeField] private Material[] _materials;
    private List<Material> _dataMeterials;
    private int _donutNumber;
    private List<Donut> _donuts;
    private Vector3 _spawnDonutPostion;

    private void Start()
    {
        SpawnDonut();
    }

    private void SpawnDonut()
    {
        _spawnDonutPostion = transform.position;
        _donutNumber = Random.Range(1, 4);
        _dataMeterials = new List<Material>(_materials);
        for (int i = 0; i < _donutNumber; i++)
        {
            Donut donut = Instantiate(_donut, _spawnDonutPostion,
                _donut.transform.rotation, transform);
            donut.transform.position = _spawnDonutPostion;
            _spawnDonutPostion += new Vector3(0, 0.325f, 0);
            int dataMaterialIndex = Random.Range(0, _dataMeterials.Count);
            donut.SetMaterial(_dataMeterials[dataMaterialIndex]);
            _dataMeterials.RemoveAt(dataMaterialIndex);
        }
    }
}
