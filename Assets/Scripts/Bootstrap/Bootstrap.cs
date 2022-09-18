using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("GameTickService")]
    public int TicksPerSecond;

    [Header("MapGenerator")] 
    [Range(16,128)]
    public int MapSize = 16;
    public Cell CellPrefab;
    public Sprite GrassSprite;

    [Header("Camera")] 
    public float ScrollSpeed;
    
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
            MapSize = MapSize,
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

        var CameraBehaviour = FindObjectOfType<CameraBehaviour>();
        CameraBehaviour.Setup(MapSize/2, ScrollSpeed);
    }
}
