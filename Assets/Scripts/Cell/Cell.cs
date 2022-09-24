using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Cell : MonoBehaviour
{
    public CellState CurrentState { get; private set; }
    public event Action<Cell, CellState> OnStateChanged;
    
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _placedObject;
    
    private Func<CellState, Sprite> _getBackgroundSprite;
    private Func<int, Sprite> _getTreeSprite;

    private int _treeGrowTicks;
    private int _tickAcc;
    public Vector2 Position { get; set; }
    
    public void Init(
        Func<CellState, Sprite> getBackgroundSprite,
        Func<int, Sprite> getSproutSprite,
        CellState state,
        int treeGrowTicks)
    {
        _getBackgroundSprite = getBackgroundSprite;
        _getTreeSprite = getSproutSprite;
        _treeGrowTicks = treeGrowTicks;
        SetState(state);
    }

    private void SetState(CellState state)
    {
        CurrentState = state;
        _background.sprite = _getBackgroundSprite(CurrentState);

        switch (state)
        {
            case CellState.Tree:
                PlaceObject(_getTreeSprite(2));
                break;
            case CellState.SproutGrowing:
                PlaceObject(_getTreeSprite(0));
                break;
            default:
                PlaceObject(null);
                break;
        }
        OnStateChanged?.Invoke(this, CurrentState);
    }

    private void PlaceObject(Sprite placedObjectSprite)
    {
        _placedObject.sprite = placedObjectSprite;
        _placedObject.transform.localPosition = UnityEngine.Random.insideUnitCircle * .2f;
    }

    public void OnTreeChopped()
    {
        SetState(CellState.Grass);
    }

    public void PlantTree()
    {
        SetState(CellState.SproutGrowing);
    }

    public void OnTick()
    {
        if (CurrentState != CellState.SproutGrowing) return;

        _tickAcc++;

        if (_tickAcc == _treeGrowTicks / 2) _placedObject.sprite = _getTreeSprite(1);

        if (_tickAcc >= _treeGrowTicks)
        {
            SetState(CellState.Tree);
            _tickAcc = 0;
        }
    }
}
