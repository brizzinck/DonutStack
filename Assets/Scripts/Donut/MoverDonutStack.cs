using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoverDonutStack : MonoBehaviour
{
    [SerializeField] private SpawDonutStack _spawDonut;
    [SerializeField] private ComboAnalyzer _comboAnalyzer;
    private bool _canShoot = true;
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
                current.transform.DOMove(row.Cells[i].transform.position + new Vector3(0, 0.2f, 0), 0.3f).
                    OnComplete(() => { _canShoot = true; ComboAnalyzer.FindMatchingStacks(); });
                row.Cells[i].IsFree = false;
                row.Cells[i].SetDonutStack(current);
                current.transform.parent = row.Cells[i].transform;
                _spawDonut.SpawnDonutStack();
                break;
            }
        }
    }
}
