using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presets/MapTileSetPreset", fileName = "MapTileSetPreset")]
public class MapTileSetPreset : ScriptableObject
{
    public Sprite Grass;
    public Sprite Ground;
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