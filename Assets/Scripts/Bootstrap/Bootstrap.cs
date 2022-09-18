using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("GameTickService")]
    public int TicksPerSecond;
    
    [Header("MapGenerator")]
    public Cell CellPrefab;
    public Sprite GrassSprite;
    
    private void Awake()
    {
        var gameTickService = new GameObject(nameof(GameTickService)).AddComponent<GameTickService>();
        gameTickService.SetTicksPerSecond(TicksPerSecond);
        
        StartDefaultGame(gameTickService);
    }

    private void StartDefaultGame(GameTickService gameTickService)
    {
        var mapSettings = new MapSettings()
        {
            MapSize = 16,
            TreeDensity = 1,
        };

        var tileSet = new Dictionary<MapGenerator.CellState, Sprite>()
        {
            {MapGenerator.CellState.Grass, GrassSprite}
        };
        
        var mapGenerator = MapGenerator.CreateMap(
            tileSet,
            CellPrefab,
            mapSettings, 
            gameTickService);
    }
}
