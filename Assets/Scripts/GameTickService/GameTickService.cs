using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTickService : MonoBehaviour
{
    private float _secondsPerTick;
    private float _timeAcc;
    private bool _isPaused = true;

    public event Action OnTick;

    private void Update()
    {
        if (_isPaused) return;
        
        _timeAcc += Time.deltaTime;
        if (!(_timeAcc >= _secondsPerTick)) return;

        _timeAcc -= _secondsPerTick;
        Tick();
    }

    public void SetTicksPerSecond(int tps) => _secondsPerTick = 1f / tps;

    private void Tick() 
    {
        OnTick?.Invoke();
    }

    public void Pause() => _isPaused = true;
    public void Play() => _isPaused = false;
}
