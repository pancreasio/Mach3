using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int width, height;
    private Grid grid;
    private GridView view;
    public bool xd;

    private void Start()
    {
        grid = new Grid();
        view = GameObject.Find("GridView").GetComponent<GridView>();
        xd = true;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid.NewGrid(width, height);
        for (int i = 0; i < width; i++)
        {
            for (int i2 = 0; i2 < height; i2++)
            {
                grid.SetChip((Grid.ChipType)Random.Range((int)0, (int)4),i,i2);
            }
        }
    }

    private void Update()
    {
        if (xd)
        {
            view.Initialize(width, height);
            view.DrawGrid(grid);
            xd = false;
        }
    }
}
