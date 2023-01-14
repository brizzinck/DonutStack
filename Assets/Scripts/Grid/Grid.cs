using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Row[] _rows;
    private Cell[,] _cells;

    public Cell[,] Cells { get => _cells; }

    private void Start()
    {
        _cells = new Cell[_rows.Length, _rows[0].Cells.Length];
        for (int i = 0; i < _rows.Length; i++)
        {
            for (int j = 0; j < _rows[i].Cells.Length; j++)
            {
                Cells[i, j] = _rows[i].Cells[j];
                Cells[i, j].Cordinate = new Vector2Int(i, j);
            }
        }
    }
}
