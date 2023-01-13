using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnalyzer : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    private static Grid grid;
    public static void FindMatchingStacks()
    {
        bool breaked = false;
        for (int i = 0; i < grid.Cells.GetLength(0); i++)
        {
            if (breaked) break;
            for (int j = 0; j < grid.Cells.GetLength(1); j++)
            {
                if (!grid.Cells[i, j].IsFree)
                {
                    breaked = CheckAndCombineWithNeighbors(i, j);
                    if (breaked) break;
                }
            }
        }
    }
    private void Awake()
    {
        grid = _grid;
    }

    private static bool CheckAndCombineWithNeighbors(int i, int j)
    {
        string color = grid.Cells[i, j].DonutStack.Donuts[grid.Cells[i, j].DonutStack.Donuts.Count - 1].ColorName;
        List<DonutStack> donutStacks = new List<DonutStack>();
        if (i - 1 < 0 || i + 1 > grid.Cells.GetLength(0) - 1 || j - 1 < 0 || j + 1 > grid.Cells.GetLength(1) - 1) return false;
        if (!grid.Cells[i - 1, j].IsFree &&
            grid.Cells[i - 1, j].DonutStack.Donuts[grid.Cells[i - 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            donutStacks.Add(grid.Cells[i - 1, j].DonutStack);
        }
        if (!grid.Cells[i + 1, j].IsFree &&
            grid.Cells[i + 1, j].DonutStack.Donuts[grid.Cells[i + 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            donutStacks.Add(grid.Cells[i + 1, j].DonutStack);
        }
        if (!grid.Cells[i, j - 1].IsFree &&
            grid.Cells[i, j - 1].DonutStack.Donuts[grid.Cells[i, j - 1].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            donutStacks.Add(grid.Cells[i, j - 1].DonutStack);
        }
        if (!grid.Cells[i, j + 1].IsFree &&
            grid.Cells[i, j + 1].DonutStack.Donuts[grid.Cells[i, j + 1].DonutStack.Donuts.Count - 1].ColorName == color)
        {
            donutStacks.Add(grid.Cells[i, j + 1].DonutStack);
        }
        if (donutStacks.Count > 0)
        {
            CombineDonutStack(grid.Cells[i, j].DonutStack, Prioritize(donutStacks));
            return true;
        }
        return false;
    }
    private static DonutStack Prioritize(List<DonutStack> donutStacks)
    {
        DonutStack donutStack = donutStacks[0];
        for (int i = 0; i < donutStacks.Count; i++)
        {
            if (i + 1 < donutStacks.Count - 1) 
            {
                if (donutStacks[i].Donuts.Count < donutStacks[i + 1].Donuts.Count)
                {
                    donutStack = donutStacks[i];
                }
            }
        }
        return donutStack;
    }
    private static DonutStack CombineDonutStack(DonutStack donut, DonutStack neighborDonuts)
    {
        neighborDonuts.MergeDonutStack(donut.Donuts[donut.Donuts.Count - 1], donut);
        return neighborDonuts;
    }
}
