using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public UnityAction<Cell> DonutScreed;
    private bool _isFree = true;
    private DonutStack _donutStack;
    public bool IsFree { get => _isFree; }
    public DonutStack DonutStack { get => _donutStack; }

    public void DonutScreeder(Cell cell)
    {
        if (cell.IsFree && _donutStack != null)
        {
            MoverDonutStack.SetParametersNewPostion(cell, _donutStack);
            SetFree();
        }
    }
    public void SetDonutStack(DonutStack donutStack)
    {
        donutStack.transform.parent = transform;
        _donutStack = donutStack;
        _isFree = false;
        _donutStack.DestroyDonutStack += SetFree;
    }
    public void SetFree()
    {
        _isFree = true;
        DonutScreed?.Invoke(this);
    }
}
