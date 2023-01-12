using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawDonutStack : MonoBehaviour
{
    [SerializeField] private DonutStack _donutStack;
    private DonutStack _currentDonutStack;
    public DonutStack CurrentDonutStack { get => _currentDonutStack; }
    public void SpawnDonutStack()
    {
        DonutStack donutStack = Instantiate(_donutStack, transform.position, Quaternion.identity);
        donutStack.transform.position = transform.position;
        _currentDonutStack = donutStack;
    }

    private void Start()
    {
        SpawnDonutStack();
    }

}
