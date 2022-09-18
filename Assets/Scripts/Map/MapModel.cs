using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    private Cell[] _cells;

    public MapModel(Cell[] cells, GameTickService gameTickService)
    {
        _cells = cells;
        gameTickService.OnTick += OnTick;
    }

    private void OnTick()
    {
        
    }
}
