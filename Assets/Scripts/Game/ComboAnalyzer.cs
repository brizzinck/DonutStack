using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ComboAnalyzer : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    private static Grid grid;
    private static bool _combination = false;

    public static bool Combination { get => _combination; set => _combination = value; }
    public static void FindMatchingStacks()
    {
        if (_combination) return;
        _combination = true;
        bool breaked = false;
        for (int i = 0; i < grid.Cells.GetLength(0); i++)
        {
            if (breaked) break;
            for (int j = 0; j < grid.Cells.GetLength(1); j++)
            {
                if (!grid.Cells[i, j].IsFree && grid.Cells[i, j].DonutStack.Donuts.Count < 3)
                {
                    breaked = CheckCombine(i, j);
                    if (breaked) break;
                }
            }
        }
        if (!breaked) 
            _combination = false;
    }
    private void Awake()
    {
        grid = _grid;
    }

    private static bool CheckCombine(int i, int j)
    {
        List<DonutStack> donutStacks = new List<DonutStack>();
        string color = grid.Cells[i, j].DonutStack.Donuts[grid.Cells[i, j].DonutStack.Donuts.Count - 1].ColorName;
        donutStacks = CheckNeighbors(i, j);
        if (donutStacks.Count > 0)
        {
            DonutStack donutStack = Prioritize(donutStacks, color);
            CheckWhoCombine(grid.Cells[i, j].DonutStack, donutStack, color);
            return true;
        }
        return false;
    }

    public static List<DonutStack> CheckNeighbors(int i, int j)
    {
        string color = grid.Cells[i, j].DonutStack.Donuts[grid.Cells[i, j].DonutStack.Donuts.Count - 1].ColorName;
        List<DonutStack> donutStacks = new List<DonutStack>();
        if (i - 1 > 0)
        {
            if (!grid.Cells[i - 1, j].IsFree &&
                grid.Cells[i - 1, j].DonutStack.Donuts[grid.Cells[i - 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
            {
                donutStacks.Add(grid.Cells[i - 1, j].DonutStack);
            }
        }
        if (i + 1 < grid.Cells.GetLength(0))
        {
            if (!grid.Cells[i + 1, j].IsFree &&
                grid.Cells[i + 1, j].DonutStack.Donuts[grid.Cells[i + 1, j].DonutStack.Donuts.Count - 1].ColorName == color)
            {
                donutStacks.Add(grid.Cells[i + 1, j].DonutStack);
            }
        }
        if (j - 1 > 0)
        {
            if (!grid.Cells[i, j - 1].IsFree &&
                        grid.Cells[i, j - 1].DonutStack.Donuts[grid.Cells[i, j - 1].DonutStack.Donuts.Count - 1].ColorName == color)
            {
                donutStacks.Add(grid.Cells[i, j - 1].DonutStack);
            }
        }
        if (j + 1 < grid.Cells.GetLength(1))
        {
            if (!grid.Cells[i, j + 1].IsFree &&
                grid.Cells[i, j + 1].DonutStack.Donuts[grid.Cells[i, j + 1].DonutStack.Donuts.Count - 1].ColorName == color)
            {
                donutStacks.Add(grid.Cells[i, j + 1].DonutStack);
            }
        }
        return donutStacks;
    }

    private static void CheckWhoCombine(DonutStack donutStack1, DonutStack donutStack, string color)
    {
        if (DonutStack.CheckColored(donutStack1, color) &&
            DonutStack.CheckColored(donutStack, color))
            CheckCount(donutStack1, donutStack, false);
        else if (DonutStack.CheckColored(donutStack1, color))
            CombineDonutStack(donutStack, donutStack1);
        else if (DonutStack.CheckColored(donutStack, color))
            CombineDonutStack(donutStack1, donutStack);
        else 
            CheckCount(donutStack1, donutStack, true);
    }

    private static void CheckCount(DonutStack donutStack1, DonutStack donutStack, bool whoCheck)
    {
        if (whoCheck)
        {
            if (donutStack.Donuts.Count < donutStack1.Donuts.Count)
                CombineDonutStack(donutStack1, donutStack);
            else
                CombineDonutStack(donutStack, donutStack1);
        }
        else if (!whoCheck)
        {
            if (donutStack.Donuts.Count < donutStack1.Donuts.Count)
                CombineDonutStack(donutStack, donutStack1);
            else
                CombineDonutStack(donutStack1, donutStack);
        }
    }

    public static DonutStack Prioritize(List<DonutStack> donutStacks, string color)
    {
        DonutStack donutStack = null;
        if (donutStacks.Count == 0) return null;
        else if (donutStacks.Count == 1) return donutStacks[0];
        for (int i = 0; i < donutStacks.Count; i++)
        {
            if (i + 1 < donutStacks.Count)
            {
                if (DonutStack.CheckColored(donutStacks[i], color) && DonutStack.CheckColored(donutStacks[i + 1], color))
                {
                    if (donutStacks[i].Donuts.Count <= donutStacks[i + 1].Donuts.Count)
                        donutStack = donutStacks[i];
                    else
                        donutStack = donutStacks[i + 1];
                }
                else if (DonutStack.CheckColored(donutStacks[i], color))
                    donutStack = donutStacks[i];
                else if (DonutStack.CheckColored(donutStacks[i + 1], color))
                    donutStack = donutStacks[i + 1];
            }
        }
        if (donutStack != null) return donutStack;
        for (int i = 0; i < donutStacks.Count; i++)
        {       
            if (i + 1 < donutStacks.Count) 
            {
                if (donutStacks[i].Donuts.Count <= donutStacks[i + 1].Donuts.Count)
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
