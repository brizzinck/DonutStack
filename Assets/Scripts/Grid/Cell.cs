using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public UnityAction<Cell> DonutScreed;
    private List<Cell> _neighboursCell = new List<Cell>();
    private Vector2Int _cordinate;
    private bool _isFree = true;
    private DonutStack _donutStack;
    public bool IsFree { get => _isFree; }    
    public DonutStack DonutStack { get => _donutStack; }
    public Vector2Int Cordinate { get => _cordinate; set => _cordinate = value; }

    public void DonutScreeder(Cell cell)
    {
        if (_donutStack != null)
        {
            MoverDonutStack.SetParametersNewPostion(cell, _donutStack);
            SetFree();
        }
    }
    public void SetDonutStack(DonutStack donutStack)
    {
        _isFree = false;
        donutStack.transform.parent = transform;
        _donutStack = donutStack;
        _donutStack.SetCell(this);
        _donutStack.DestroyDonutStack += SetFree;
    }
    public void SetFree()
    {
        if (_donutStack != null)
            _donutStack.DestroyDonutStack -= SetFree;
        _isFree = true;
        _donutStack = null;
        DonutScreed?.Invoke(this);
    }
}
