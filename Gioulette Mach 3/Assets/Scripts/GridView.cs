using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    public GameObject redPrefab, greenPrefab, bluePrefab, pinkPrefab;
    private int width, height;
    private GameObject[,] goArray;

    public void Initialize(int x, int y)
    {
        goArray = new GameObject[x, y];
        width = x;
        height = y;
    }

    public void DrawGrid(Grid grid)
    {
        for (int i = 0; i < width; i++)
        {
            for (int i2 = 0; i2 < height; i2++)
            {
                DrawChip(grid.GetChip(i, i2), i, i2);
                Debug.Log("drawing chip: " + i + " " + i2);
            }
        }
    }

    private void DrawChip(Grid.ChipType chip, int x, int y)
    {
        float newX = new float();
        float newY = new float();
        newX = -2.0f + x * 0.571f;
        newY = -4.2f + y * 0.97f;
        switch (chip)
        {
            case Grid.ChipType.R:
                Instantiate(redPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.G:
                Instantiate(greenPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.B:
                Instantiate(bluePrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.P:
                Instantiate(pinkPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
