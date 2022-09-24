using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    private Cell[] _cells;

    private List<Woodsman> _workers;
    private List<Cell> _freeTrees;
    private int _ticksBetweenTreeSpawn;
    private int _tickAcc;

    private CellState[][] _cellStates;
    private List<Cell> _treeGrowthCandidates = new List<Cell>();
    
    public MapModel(
        Cell[] cells, 
        GameTickService gameTickService,
        MapSettings mapSettings)
    {
        _cells = cells;
        _ticksBetweenTreeSpawn = mapSettings.TicksBetweenTreeSpawn;

        InitStateMap(mapSettings.MapSize, _cells);

        gameTickService.OnTick += OnTick;
        _workers = new List<Woodsman>();
        _freeTrees = new List<Cell>();
        
        foreach (var cell in _cells)
        {
            cell.OnStateChanged += OnCellStateChanged;
            gameTickService.OnTick += cell.OnTick;
            if (cell.CurrentState == CellState.Tree)
                _freeTrees.Add(cell);
        }
    }

    private void InitStateMap(int mapSize, Cell[] cells)
    {
        _cellStates = new CellState[mapSize][];
        for (var x = 0; x < mapSize; x++)
        {
            _cellStates[x] = new CellState[mapSize];
            for (var y = 0; y < mapSize; y++)
            {
                _cellStates[x][y] = cells[x * mapSize + y].CurrentState;
                if (_cellStates[x][y] == CellState.Grass)
                    _treeGrowthCandidates.Add(cells[x * mapSize + y]);
            }
        }
    }

    public Cell[] GetCells() => _cells;

    public List<Woodsman> GetWorkers() => _workers;

    private void OnTick()
    {
        _workers.ForEach(worker =>
        {
            worker.OnTick();
        });

        _tickAcc++;
        if (_tickAcc < _ticksBetweenTreeSpawn) return;
        
        CreateSprout();
        _tickAcc -= _ticksBetweenTreeSpawn;
    }

    private void OnCellStateChanged(Cell cell, CellState newState)
    {
        switch (newState)
        {
            case CellState.Tree:
                _treeGrowthCandidates.Remove(cell);
                _freeTrees.Add(cell);
                break;
            case CellState.Grass:
                _treeGrowthCandidates.Add(cell);
                break;
        }
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

    private void CreateSprout()
    {
        if (_treeGrowthCandidates.TryExtractRandom(out var cell))
            cell.PlantTree();
    }
}
