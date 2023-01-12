using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverDonutStack : MonoBehaviour
{
    [SerializeField] private SpawDonutStack spawDonut;
    private bool _canShoot = true;
    public void MoveDonutStack(Transform transform)
    {
        Row row = transform.GetComponent<Row>();
        if (!_canShoot || row == null) return;
        for (int i = 0; i < row.Cells.Length; i++)
        {
            if (row.Cells[i] != null && row.Cells[i].IsFree)
            {
                _canShoot = false;
                spawDonut.CurrentDonutStack.transform.position = new Vector3(
                    row.Cells[i].transform.position.x,
                    spawDonut.CurrentDonutStack.transform.position.y,
                    spawDonut.CurrentDonutStack.transform.position.z);
                spawDonut.CurrentDonutStack.transform.DOMove(row.Cells[i].transform.position, 0.3f).
                    OnComplete(() => _canShoot = true);
                row.Cells[i].IsFree = false;
                spawDonut.SpawnDonutStack();
                break;
            }
        }
    }
}
