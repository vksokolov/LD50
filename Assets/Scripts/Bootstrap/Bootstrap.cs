using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Presets")] 
    [SerializeField] private MapSettings MapSettings;
    [SerializeField] private WoodsmanStats WoodsmanStats;
    [SerializeField] private MapTileSetPreset TileSetPreset;
    
    [Header("GameTickService")]
    public int TicksPerSecond;

    [Header("MapGenerator")] 
    public Cell CellPrefab;

    [Header("MiniMap")] 
    public MiniMap MiniMap;
    public int TicksToRedraw;

    [Header("Camera")] 
    public float ScrollSpeed;

    [Header("Workers")] 
    public Woodsman WoodsmanPrefab;
    public int WoodsmanCount;
    
    private int MapSize => MapSettings.MapSize;
    
    private void Awake()
    {
        var gameTickService = new GameObject(nameof(GameTickService)).AddComponent<GameTickService>();
        gameTickService.SetTicksPerSecond(TicksPerSecond);
        
        StartDefaultGame(gameTickService);
    }

    private void StartDefaultGame(GameTickService gameTickService)
    {
        var mapModel = MapGenerator.CreateMap(
            TileSetPreset.GetTileSet(),
            WoodsmanCount,
            WoodsmanPrefab,
            WoodsmanStats,
            CellPrefab,
            MapSettings, 
            gameTickService);

        var cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        cameraBehaviour.Setup(MapSize/2, ScrollSpeed);
        
        MiniMap.Init(mapModel, TicksToRedraw);
        gameTickService.OnTick += MiniMap.OnTick;
        
        gameTickService.Play();
    }
}
