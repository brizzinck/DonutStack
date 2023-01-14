using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public UnityAction<Cell> DonutScreed;
    private Vector2Int _cordinate;
    private bool _isFree = true;
    private DonutStack _donutStack;
    public bool IsFree { get => _isFree; }
    public DonutStack DonutStack { get => _donutStack; }
    public Vector2Int Cordinate { get => _cordinate; set => _cordinate = value; }

    public void DonutScreeder(Cell cell)
    {
        SetFree();
        if (cell.IsFree && _donutStack != null)
        {
            MoverDonutStack.SetParametersNewPostion(cell, _donutStack);
        }
    }
    public void SetDonutStack(DonutStack donutStack)
    {
        donutStack.transform.parent = transform;
        _donutStack = donutStack;
        _donutStack.SetCell(this);
        _isFree = false;
        _donutStack.DestroyDonutStack += SetFree;
    }
    public void SetFree()
    {
        _isFree = true;
        DonutScreed?.Invoke(this);
    }
}
