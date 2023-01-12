using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    public Cell[] Cells { get => _cells; }
}