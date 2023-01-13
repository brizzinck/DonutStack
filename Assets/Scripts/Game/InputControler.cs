using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    [SerializeField] private MoverDonutStack _moverDonutStack;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        CheckMove();
    }

    private void CheckMove()
    {
        if (ValidatePlayerTouch())
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit == null) return;
                _moverDonutStack.MoveDonutStack(objectHit);
            }
        }
    }

    private bool ValidatePlayerTouch()
    {
        return Input.GetMouseButtonDown(0);
    }
}
