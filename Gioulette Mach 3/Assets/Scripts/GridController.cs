﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private Grid grid;
    private GridView view;

    private void Start()
    {
        view = GameObject.Find("GridView").GetComponent<GridView>();
    }

    private void Update()
    {
        
    }
}
