using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MapGenerator
{
    public static MapModel CreateMap(
        MapTileSetPreset tileSetPreset,
        int woodsmanCount,
        Woodsman woodsmanPrefab,
        WoodsmanStats woodsmanStats,
        MapSettings settings,
        GameTickService gameTickService)
    {
        int size = settings.MapSize;
        float minusHalf = -(size / 2) + .5f;
        Cell[] cells = new Cell[size * size];

        var tileSet = tileSetPreset.GetTileSet();
        var cellPrefab = tileSetPreset.CellPrefab;
        var mapRoot = new GameObject("Map").transform;
        
        for (var x = 0; x < settings.MapSize; x++)
            for (var y = 0; y < settings.MapSize; y++)
            {
                float random = UnityEngine.Random.Range(0, 1);
                CellState state = CellState.Grass;
                if (random < settings.TreeDensity)
                    state = CellState.Tree;
                
                var cell = CreateCell(state, new Vector2(x, y));
                
                var transform = cell.transform;
                transform.SetParent(mapRoot);
                transform.position = new Vector3(minusHalf + x, minusHalf + y);
                
                cells[x * size + y] = cell;
            }

        var map = new MapModel(cells, gameTickService, settings);

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

        Cell CreateCell(CellState state, Vector2 position)
        {
            var cell = Object.Instantiate(cellPrefab);
            cell.Position = position;

            Sprite GetBackgroundSprite(CellState x)
            {
                switch (x)
                {
                    case CellState.Grass:
                    case CellState.Tree:
                    case CellState.SproutGrowing:
                        return tileSet[CellState.Grass];
                    default:
                        return tileSet[CellState.Ground];
                }
            }
            
            Sprite GetTreeGrowthSprite(int x)
            {
                if (x == 0) return tileSetPreset.Sprout1;
                if (x == 1) return tileSetPreset.Sprout2;
                if (x == 2) return tileSetPreset.Tree;
                throw new Exception($"Cant find a sprout sprite with index {x}");
            }
            cell.Init(
                GetBackgroundSprite, 
                GetTreeGrowthSprite,
                state,
                settings.TreeGrowthTicks);

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
