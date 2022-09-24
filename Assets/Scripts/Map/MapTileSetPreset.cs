using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presets/MapTileSetPreset", fileName = "MapTileSetPreset")]
public class MapTileSetPreset : ScriptableObject
{
    [Header("Prefabs")]
    public Cell CellPrefab;
    
    [Header("Tiles")]
    [Space]
    public Sprite Grass;
    public Sprite Ground;
    
    [Header("Sprouts")]
    [Space]
    public Sprite Sprout1;
    public Sprite Sprout2;
    public Sprite Tree;
    
    //public TileSetDictionary TileSet;

    public Dictionary<CellState, Sprite> GetTileSet()
    {
        var tileSet = new Dictionary<CellState, Sprite>()
        {
            {CellState.Grass, Grass},
            {CellState.Ground, Ground},
            {CellState.Tree, Tree}
        };

        return tileSet;
    }
}

[Serializable]
public class TileSetDictionary : UnitySerializedDictionary<CellState, Sprite> {}