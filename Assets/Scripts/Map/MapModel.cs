using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    private Cell[] _cells;

    private List<Woodsman> _workers;
    private List<Cell> _freeTrees;
    
    public MapModel(Cell[] cells, GameTickService gameTickService)
    {
        _cells = cells;
        gameTickService.OnTick += OnTick;
        _workers = new List<Woodsman>();
        _freeTrees = new List<Cell>();
        
        foreach (var cell in _cells)
        {
            if (cell.CurrentState == CellState.Tree)
                _freeTrees.Add(cell);
        }
    }

    private void OnTick()
    {
        _workers.ForEach(worker =>
        {
            worker.OnTick();
        });
    }

    private void OnCellStateChanged(Cell cell, CellState newState)
    {
        
    }
    
    public void AddWorker(Woodsman woodsman)
    {
        _workers.Add(woodsman);
    }

    public Cell GetTree(Vector3 closestTo)
    {
        if (_freeTrees.Count <= 0) return null;

        var cell = _freeTrees[0];
        var minSqrMagnitude = (cell.transform.position - closestTo).sqrMagnitude;
        _freeTrees.ForEach(tree =>
        {
            var sqrMagnitude = (tree.transform.position - closestTo).sqrMagnitude;
            if (!(minSqrMagnitude > sqrMagnitude)) return;
            cell = tree;
            minSqrMagnitude = sqrMagnitude;
        });
        
        _freeTrees.Remove(cell);
        return cell;
    }

    public void TurnInWood(int amount)
    {
        
    }
}
