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
    public Sprite TreeSprite;

    [Header("Camera")] 
    public float ScrollSpeed;

    [Header("Workers")] 
    public Woodsman WoodsmanPrefab;
    public float Speed = 2;
    public int ChopTimeTicks = 5;
    public int LumbersPerTree = 1;
    public Vector3 HomePosition = new Vector3(1, 1, 0);
    
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

        var tileSet = new Dictionary<CellState, Sprite>()
        {
            {CellState.Grass, GrassSprite},
            {CellState.Tree, TreeSprite}
        };

        var woodsmanStats = new WoodsmanStats(
            ChopTimeTicks,
            LumbersPerTree,
            Speed,
            HomePosition,
            null,
            null
            );
        
        var mapGenerator = MapGenerator.CreateMap(
            tileSet,
            WoodsmanPrefab,
            woodsmanStats,
            CellPrefab,
            mapSettings, 
            gameTickService);

        var cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        cameraBehaviour.Setup(MapSize/2, ScrollSpeed);
        
        gameTickService.Play();
    }
}
