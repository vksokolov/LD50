using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MapGenerator
{
    public enum CellState
    {
        Ground,
        Grass,
        Tree,
        Building,
    }
    
    public static MapModel CreateMap(
        Dictionary<CellState, Sprite> tileSet,
        Cell cellPrefab,
        MapSettings settings,
        GameTickService gameTickService)
    {
        int size = settings.MapSize;
        int minusHalf = -size / 2;
        Cell[] cells = new Cell[size * size];
        
        for (var x = 0; x < settings.MapSize; x++)
            for (var y = 0; y < settings.MapSize; y++)
            {
                var cell = CreateCell(CellState.Grass);
                cell.transform.position = new Vector3(minusHalf + x, minusHalf + y);
                cells[x * size + y] = cell;
            }

        var map = new MapModel(cells, gameTickService);
        return map;

        Cell CreateCell(CellState state)
        {
            var cell = Object.Instantiate(cellPrefab);
            cell.SetSprite(tileSet[state]);

            return cell;
        }
    }
}
