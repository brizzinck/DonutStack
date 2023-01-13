using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoverDonutStack : MonoBehaviour
{
    [SerializeField] private SpawDonutStack _spawDonut;
    [SerializeField] private ComboAnalyzer _comboAnalyzer;
    private static bool _canShoot = true;
    public void MoveDonutStack(Transform transform)
    {
        Row row = transform.GetComponent<Row>();
        if (!_canShoot || row == null) return;
        for (int i = 0; i < row.Cells.Length; i++)
        {
            if (row.Cells[i] != null && row.Cells[i].IsFree)
            {
                DonutStack current = _spawDonut.CurrentDonutStack;
                _canShoot = false;
                current.transform.position = new Vector3(
                    row.Cells[i].transform.position.x,
                    current.transform.position.y,
                    current.transform.position.z);
                SetParametersNewPostion(row.Cells[i], current);
                _spawDonut.SpawnDonutStack();
                break;
            }
        }
    }

    public static void SetParametersNewPostion(Cell cell, DonutStack donutStack)
    {
        cell.SetDonutStack(donutStack);
        AnimationMove(cell.transform.position, donutStack);
    }

    private static void AnimationMove(Vector3 position, DonutStack donutStack)
    {
        donutStack.transform.DOMove(position + new Vector3(0, 0.2f, 0), 0.3f).
                            OnComplete(() => { _canShoot = true; ComboAnalyzer.FindMatchingStacks(); });
    }
}
