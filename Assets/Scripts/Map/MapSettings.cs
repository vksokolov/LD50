using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Presets/MapSettings", fileName = "MapSettings")]
public class MapSettings : ScriptableObject
{
    [Range(0,1f)]
    public float TreeDensity;
    [Range(16,128)]
    public int MapSize;
}