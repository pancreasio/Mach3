using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public enum ChipType {R,G,B,P};

    private ChipType [,]chipInstances = new ChipType [7,7];

    public void SetChip(ChipType newChip, int x, int y)
    {
        chipInstances[x, y] = newChip;
    }

    public ChipType GetChip(int x, int y)
    {
        return chipInstances[x, y];
    }
}
