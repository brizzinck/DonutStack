using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public UnityAction DonutScreed;
    private bool _isFree = true;
    private DonutStack _donutStack;
    public bool IsFree { get => _isFree; set => _isFree = value; }
    public DonutStack DonutStack { get => _donutStack; }

    public void SetDonutStack(DonutStack donutStack)
    {
        _donutStack = donutStack;
        _donutStack.DestroyDonutStack += SetFree;
    }
    public void SetFree()
    {
        _isFree = true;
        DonutScreed?.Invoke();
    }
}
