using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellState CurrentState { get; private set; }
    public event Action<Cell, CellState> OnStateChanged;
    
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private Func<CellState, Sprite> _getSprite;

    public void Init(Func<CellState, Sprite> getSprite,
        CellState state)
    {
        _getSprite = getSprite;
        SetState(state);
    }

    private void SetState(CellState state)
    {
        CurrentState = state;
        _spriteRenderer.sprite = _getSprite(CurrentState);
        
        OnStateChanged?.Invoke(this, CurrentState);
    }

    public void OnTreeChopped()
    {
        SetState(CellState.Grass);
    }
}
