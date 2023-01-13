using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    private string _colorName = string.Empty;

    public string ColorName { get => _colorName; }

    public void SetMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
        _colorName = material.name;
    }
}
