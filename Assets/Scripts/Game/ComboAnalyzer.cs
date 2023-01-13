using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnalyzer : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    public void FindMatchingStacks(DonutStack donutStack)
    {
        for (int i = 0; i < _grid.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.Cells.GetLength(1); j++)
            {
                if (!_grid.Cells[i, j].IsFree)
                {
                    CheckAndCombineWithNeighbors(i, j);
                }
            }
        }
    }
    private void CheckAndCombineWithNeighbors(int i, int j)
    {
        string color = _grid.Cells[i, j].DonutStack.Donuts[_grid.Cells[i, j].DonutStack.Donuts.Count - 1].ColorName;
        if (i - 1 < 0 || i + 1 > _grid.Cells.GetLength(0) - 1 || j - 1 < 0 || j + 1 > _grid.Cells.GetLength(1) - 1) return;
        if (!_grid.Cells[i - 1, j].IsFree &&
            _grid.Cells[i - 1, j].DonutStack.Donuts[_grid.Cells[i - 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            CombineDonutStack(_grid.Cells[i, j].DonutStack, _grid.Cells[i - 1, j].DonutStack);
        }
        if (!_grid.Cells[i + 1, j].IsFree &&
            _grid.Cells[i + 1, j].DonutStack.Donuts[_grid.Cells[i + 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            CombineDonutStack(_grid.Cells[i, j].DonutStack, _grid.Cells[i + 1, j].DonutStack);
        }
        if (!_grid.Cells[i, j - 1].IsFree &&
            _grid.Cells[i, j - 1].DonutStack.Donuts[_grid.Cells[i, j - 1].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            CombineDonutStack(_grid.Cells[i, j].DonutStack, _grid.Cells[i, j - 1].DonutStack);
        }
        if (!_grid.Cells[i, j + 1].IsFree &&
            _grid.Cells[i, j + 1].DonutStack.Donuts[_grid.Cells[i, j + 1].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            CombineDonutStack(_grid.Cells[i, j].DonutStack, _grid.Cells[i, j + 1].DonutStack);
        }
    }
    private DonutStack CombineDonutStack(DonutStack donut, DonutStack neighborDonuts)
    {
        neighborDonuts.MergeDonutStack(donut.Donuts[donut.Donuts.Count - 1], donut);
        return neighborDonuts;
    }
}
