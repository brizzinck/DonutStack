using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DonutStack : MonoBehaviour
{
    public UnityAction DestroyDonutStack;
    [SerializeField] private Donut _donut;
    [SerializeField] private Material[] _materials;
    private List<Material> _dataMeterials;
    private int _donutNumber;
    private List<Donut> _donuts = new List<Donut>();
    private Vector3 _spawnDonutPostion;

    public List<Donut> Donuts { get => _donuts; }
    public void MergeDonutStack(Donut donut, DonutStack donutStack)
    {
        donut.transform.parent = transform;
        donut.transform.DOMove(_donuts[_donuts.Count - 1].transform.position 
            + new Vector3(0, 0.325f, 0), 0.3f).OnComplete(() => { Remove(donutStack); _donuts.Add(donut); });
        CheckFullStack(this);
    }
    public void Remove(DonutStack donut)
    {
        donut.Donuts.RemoveAt(donut.Donuts.Count - 1);
        if (donut.Donuts.Count == 0)
        {
            Destroy(donut);
        }
    }
    private void Start()
    {
        _spawnDonutPostion = transform.position;
        SpawnDonut();
    }
    private void SpawnDonut()
    {
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
            _donuts.Add(donut);
        }
    }
    private void CheckFullStack(DonutStack donut)
    {
        if (donut.Donuts.Count == 3)
        {
            bool colorCheck = true;
            string colorName = donut.Donuts[0].ColorName;
            for (int i = 0; i < donut.Donuts.Count; i++)
            {
                if (donut.Donuts[i].ColorName == colorName) continue;
                colorCheck = false;
            }
            if (colorCheck)
            {
                Destroy(donut);
            }
        }
    }
    private static void Destroy(DonutStack donut)
    {
        donut.DestroyDonutStack?.Invoke();
        Destroy(donut.gameObject);
    }
}
