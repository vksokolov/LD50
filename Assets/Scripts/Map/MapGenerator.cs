using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MapGenerator
{
    public static MapModel CreateMap(
        Dictionary<CellState, Sprite> tileSet,
        int woodsmanCount,
        Woodsman woodsmanPrefab,
        WoodsmanStats woodsmanStats,
        Cell cellPrefab,
        MapSettings settings,
        GameTickService gameTickService)
    {
        int size = settings.MapSize;
        float minusHalf = -(size / 2) + .5f;
        Cell[] cells = new Cell[size * size];

        var mapRoot = new GameObject("Map").transform;
        for (var x = 0; x < settings.MapSize; x++)
            for (var y = 0; y < settings.MapSize; y++)
            {
                float random = UnityEngine.Random.Range(0, 1);
                CellState state = CellState.Grass;
                if (random < settings.TreeDensity)
                    state = CellState.Tree;
                
                var cell = CreateCell(state);
                
                var transform = cell.transform;
                transform.SetParent(mapRoot);
                transform.position = new Vector3(minusHalf + x, minusHalf + y);
                
                cells[x * size + y] = cell;
            }

        var map = new MapModel(cells, gameTickService);

        Vector3 homePosition = new Vector3(settings.MapSize * .5f, 1 - settings.MapSize * .5f, 0);
        woodsmanStats.Init(
            homePosition,
            map.GetTree,
            map.TurnInWood);

        for (var i = 0; i < woodsmanCount; i++)
        {
            map.AddWorker(CreateWoodsman());
        }
        
        return map;

        Cell CreateCell(CellState state)
        {
            var cell = Object.Instantiate(cellPrefab);

            Sprite GetSprite(CellState x) => tileSet[x];
            
            cell.Init(GetSprite, state);

            return cell;
        }
        
        Woodsman CreateWoodsman()
        {
            var woodsman = Object.Instantiate(woodsmanPrefab);
            woodsman.transform.position = woodsmanStats.HomePosition;
            woodsman.Init(woodsmanStats);

            return woodsman;
        }
    }
}
