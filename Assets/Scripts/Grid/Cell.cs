using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool _isFree = true;
    public bool IsFree { get => _isFree; set => _isFree = value; }
}
