using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int width, height;
    public LayerMask raycastLayer;
    private Grid grid;
    private Grid.ChipType storedChip;
    private GridView view;
    public bool initialized;
    private bool isChipStored;
    private Vector2Int storedPosition;

    private void Start()
    {
        grid = new Grid();
        view = GameObject.Find("GridView").GetComponent<GridView>();
        initialized = false;
        GenerateGrid();
        isChipStored = false;
        storedPosition = new Vector2Int();
    }

    private void GenerateGrid()
    {
        grid.NewGrid(width, height);
        for (int i = 0; i < width; i++)
        {
            for (int i2 = 0; i2 < height; i2++)
            {
                grid.SetChip((Grid.ChipType)Random.Range((int)0, (int)4), i, i2);
            }
        }
    }

    private void Update()
    {
        if (!initialized)
        {
            view.Initialize(width, height);
            view.DrawGrid(grid);
            initialized = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Vector2Int chipPosition = view.GetChipPosition(hit.transform.gameObject);
                if (!isChipStored)
                {
                    storedChip = grid.GetChip(chipPosition.x, chipPosition.y);
                    storedPosition = chipPosition;
                    Debug.Log("stored");
                    isChipStored = true;
                }
                else
                {
                    grid.SetChip(grid.GetChip(chipPosition.x, chipPosition.y), storedPosition.x, storedPosition.y);
                    grid.SetChip(storedChip, chipPosition.x, chipPosition.y);
                    isChipStored = false;
                    Debug.Log("swapped");
                    view.EraseGrid();
                    view.DrawGrid(grid);
                    Debug.Log("redrawn");
                }
            }
        }
    }
}
