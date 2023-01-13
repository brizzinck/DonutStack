using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    public Cell[] Cells { get => _cells; }

    private void Start()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (i + 1 < _cells.Length - 1)
            {
                _cells[i].DonutScreed += _cells[i + 1].DonutScreeder;
            }
        }
    }
}
