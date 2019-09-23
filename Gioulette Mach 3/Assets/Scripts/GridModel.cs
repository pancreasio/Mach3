using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public enum ChipType {R,G,B,P,NULLCHIP};
    private ChipType [,] chipInstances;

    public Grid NewGrid(int x, int y) {
        chipInstances = new ChipType[x, y];
        return this;
    }

    public void SetChip(ChipType newChip, int x, int y)
    {
        chipInstances[x, y] = newChip;
    }

    public ChipType GetChip(int x, int y)
    {
        return chipInstances[x, y];
    }
}
