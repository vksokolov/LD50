using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodsmanStats
{
    public int ChopTimeTicks;
    public int LumbersPerTree;
    public float Speed;
    public Vector3 HomePosition;
    
    private Func<Vector3, Cell> _getClosestTree;
    private Action<int> _turnInWood;

    public WoodsmanStats(
        int chopTimeTicks,
        int lumbersPerTree,
        float speed,
        Vector3 homePosition,
        Func<Vector3, Cell> getClosestTree,
        Action<int> turnInWood)
    {
        ChopTimeTicks = chopTimeTicks;
        LumbersPerTree = lumbersPerTree;
        Speed = speed;
        HomePosition = homePosition;
        _getClosestTree = getClosestTree;
        _turnInWood = turnInWood;
    }

    public Cell GetTree(Vector3 position) => _getClosestTree(position);
    
    public void TurnInWood(int amount) => _turnInWood(amount);
}
