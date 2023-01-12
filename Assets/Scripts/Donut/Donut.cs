using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    public void SetMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }
}
