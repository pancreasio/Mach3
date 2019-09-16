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
                goArray[i,i2] = InstantiateChip(grid.GetChip(i, i2), i, i2);
            }
        }
    }

    public void EraseGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int i2 = 0; i2 < height; i2++)
            {
                Destroy(goArray[i, i2].gameObject);
            }
        }
    }

    private GameObject InstantiateChip(Grid.ChipType chip, int x, int y)
    {
        float newX = new float();
        float newY = new float();
        newX = -2.0f + x * 0.571f;
        newY = -4.2f + y * 0.97f;
        GameObject resultGO;
        switch (chip)
        {
            case Grid.ChipType.R:
                resultGO = Instantiate(redPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.G:
                resultGO = Instantiate(greenPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.B:
                resultGO = Instantiate(bluePrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            case Grid.ChipType.P:
                resultGO = Instantiate(pinkPrefab, new Vector2(newX, newY), Quaternion.identity);
                break;
            default:
                resultGO = null;
                break;
        }
        return resultGO;
    }

    public Vector2Int GetChipPosition(GameObject chipGO)
    {
        Vector2Int resultPosition = new Vector2Int();
        for (int i = 0; i < width; i++)
        {
            for (int i2 = 0; i2 < height; i2++)
            {
                if (goArray[i, i2] == chipGO)
                {
                    resultPosition.x = i;
                    resultPosition.y = i2;
                }
            }
        }

        return resultPosition;
    }
}
