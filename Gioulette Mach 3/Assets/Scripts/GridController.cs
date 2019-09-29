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
        GenerateGrid();
        for (int i = 0; i < 100; i++)
        {
            if (!SpawnCheck())
            {
                Debug.Log("total checks: " + i + 1);
                i = 100;
            }
        }
        view = GameObject.Find("GridView").GetComponent<GridView>();
        initialized = false;
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

    private void SwapChip()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            Vector2Int chipPosition = view.GetChipPosition(hit.transform.gameObject);
            if (!isChipStored)
            {
                storedChip = grid.GetChip(chipPosition.x, chipPosition.y);
                storedPosition = chipPosition;
                isChipStored = true;
            }
            else
            {
                if (Mathf.Abs(chipPosition.x - storedPosition.x) == 1 && Mathf.Abs(chipPosition.y - storedPosition.y) == 0
                    || Mathf.Abs(chipPosition.x - storedPosition.x) == 0 && Mathf.Abs(chipPosition.y - storedPosition.y) == 1)
                {
                    grid.SetChip(grid.GetChip(chipPosition.x, chipPosition.y), storedPosition.x, storedPosition.y);
                    grid.SetChip(storedChip, chipPosition.x, chipPosition.y);
                    isChipStored = false;
                    CheckAndClearLine(storedPosition.x, true);
                    CheckAndClearLine(storedPosition.y, false);
                    CheckAndClearLine(chipPosition.x, true);
                    CheckAndClearLine(chipPosition.y, false);
                    view.EraseGrid();
                    view.DrawGrid(grid);
                }
                else
                {
                    isChipStored = false;
                }
            }
        }
    }

    private void CheckAndClearLine(int line, bool vertical)
    {
        uint repeatCounter = 0;
        Grid.ChipType previousChip = Grid.ChipType.NULLCHIP;
        List<Vector2Int> clearList = new List<Vector2Int>();

        if (vertical)
        {
            for (int heightCount = 0; heightCount < height; heightCount++)
            {
                if (grid.GetChip(line, heightCount) == previousChip)
                {
                    repeatCounter++;
                    if (repeatCounter > 1)
                    {
                        clearList.Add(new Vector2Int(line, heightCount));

                        if (repeatCounter == 2)
                        {
                            clearList.Add(new Vector2Int(line, heightCount - 1));
                            clearList.Add(new Vector2Int(line, heightCount - 2));
                        }
                    }
                }
                else
                {
                    repeatCounter = 0;
                    previousChip = grid.GetChip(line, heightCount);
                }
            }
        }
        else
        {
            for (int widthCount = 0; widthCount < width; widthCount++)
            {
                if (grid.GetChip(widthCount, line) == previousChip)
                {
                    repeatCounter++;
                    if (repeatCounter > 1)
                    {
                        if (!clearList.Contains(new Vector2Int(widthCount, line)))
                            clearList.Add(new Vector2Int(widthCount, line));

                        if (repeatCounter == 2)
                        {
                            if (!clearList.Contains(new Vector2Int(widthCount - 1, line)))
                                clearList.Add(new Vector2Int(widthCount - 1, line));

                            if (!clearList.Contains(new Vector2Int(widthCount - 2, line)))
                                clearList.Add(new Vector2Int(widthCount - 2, line));
                        }
                    }
                }
                else
                {
                    repeatCounter = 0;
                    previousChip = grid.GetChip(widthCount, line);
                }
            }
        }

        //clear repeats
        foreach (Vector2Int chipPosition in clearList)
        {
            grid.SetChip(Grid.ChipType.NULLCHIP, chipPosition.x, chipPosition.y);
        }
    }

    private bool SpawnCheck()
    {
        List<Vector2Int> rerollList = new List<Vector2Int>();
        uint repeatCounter = 0;
        bool result = false;
        Grid.ChipType previousChip = Grid.ChipType.NULLCHIP;

        //check vertical matches
        for (int widthCount = 0; widthCount < width; widthCount++)
        {
            for (int heightCount = 0; heightCount < height; heightCount++)
            {
                if (grid.GetChip(widthCount, heightCount) == previousChip)
                {
                    repeatCounter++;
                    if (repeatCounter > 1)
                    {
                        rerollList.Add(new Vector2Int(widthCount, heightCount));
                        result = true;

                        if (repeatCounter == 3)
                        {
                            rerollList.Add(new Vector2Int(widthCount, heightCount - 1));
                            rerollList.Add(new Vector2Int(widthCount, heightCount - 2));
                        }
                    }
                }
                else
                {
                    repeatCounter = 0;
                    previousChip = grid.GetChip(widthCount, heightCount);
                }
            }
            previousChip = Grid.ChipType.NULLCHIP;
            repeatCounter = 0;
        }

        //check horizontal matches
        for (int heightCount = 0; heightCount < height; heightCount++)
        {
            for (int widthCount = 0; widthCount < width; widthCount++)
            {
                if (grid.GetChip(widthCount, heightCount) == previousChip)
                {
                    repeatCounter++;
                    if (repeatCounter > 1)
                    {
                        if (!rerollList.Contains(new Vector2Int(widthCount, heightCount)))
                            rerollList.Add(new Vector2Int(widthCount, heightCount));

                        result = true;

                        if (repeatCounter == 3)
                        {
                            if (!rerollList.Contains(new Vector2Int(widthCount - 1, heightCount)))
                                rerollList.Add(new Vector2Int(widthCount - 1, heightCount));

                            if (!rerollList.Contains(new Vector2Int(widthCount - 2, heightCount)))
                                rerollList.Add(new Vector2Int(widthCount - 2, heightCount));
                        }
                    }
                }
                else
                {
                    repeatCounter = 0;
                    previousChip = grid.GetChip(widthCount, heightCount);
                }

            }
            previousChip = Grid.ChipType.NULLCHIP;
            repeatCounter = 0;
        }


        //reroll matching chips
        foreach (Vector2Int chipPosition in rerollList)
        {
            previousChip = grid.GetChip(chipPosition.x, chipPosition.y);
            while (previousChip == grid.GetChip(chipPosition.x, chipPosition.y))
            {
                grid.SetChip((Grid.ChipType)Random.Range((int)0, (int)4), chipPosition.x, chipPosition.y);
            }
        }
        return result;
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
            SwapChip();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnCheck();
            view.EraseGrid();
            view.DrawGrid(grid);
        }
    }
}
