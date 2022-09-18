using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presets/WoodsmanStats", fileName = "WoodsmanStats")]
public class WoodsmanStats : ScriptableObject
{
    public int ChopTimeTicks;
    public int LumbersPerTree;
    public float Speed;
    public Vector3 HomePosition { get; private set; }
    
    private Func<Vector3, Cell> _getClosestTree;
    private Action<int> _turnInWood;

    public Cell GetTree(Vector3 position) => _getClosestTree(position);
    
    public void TurnInWood(int amount) => _turnInWood(amount);

    public void Init(
        Vector3 homePosition,
        Func<Vector3, Cell> getClosestTree,
        Action<int> turnInWood)
    {
        HomePosition = homePosition;
        _getClosestTree = getClosestTree;
        _turnInWood = turnInWood;
    }
}
