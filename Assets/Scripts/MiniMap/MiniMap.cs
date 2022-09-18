using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    private MapModel _mapModel;
    private int _ticksToRedraw;
    private int _ticksPassed;
    
    public RawImage Image;
    
    public void Init(MapModel mapModel, int ticksToRedraw)
    {
        _mapModel = mapModel;
        _ticksToRedraw = ticksToRedraw;
    }
    
    public void OnTick()
    {
        _ticksPassed++;
        if (_ticksPassed < _ticksToRedraw) return;
        
        _ticksPassed -= _ticksToRedraw;
        RedrawMiniMap();
    }

    private void RedrawMiniMap()
    {
        int mapSize = 512;
        var cells = _mapModel.GetCells();
        int width = (int)Mathf.Sqrt(cells.Length);
        int pixelSize = mapSize / width;

        var texture = new Texture2D(512, 512, TextureFormat.RGB24, false);
        for (var x = 0; x < mapSize; x++)
        {
            for (var y = 0; y < mapSize; y++)
            {
                var cx = x / pixelSize;
                var cy = y / pixelSize;
                var cell = cells[cy + cx * width];

                Color color;
                switch (cell.CurrentState)
                {
                    case CellState.Grass:
                        color = Color.green;
                        break;
                    case CellState.Tree:
                        color = new Color(0f, 0.31f, 0.08f);
                        break;
                    default:
                        color = Color.black;
                        break;
                }
                texture.SetPixel(x, y, color);
            }
        }
        
        var workerColor = Color.blue;
        var workerRadius = 3;
        
        _mapModel.GetWorkers().ForEach(worker =>
        {
            var position = worker.transform.position;
            int cx = 255+(int)(position.x * pixelSize);
            int cy = 255+(int)(position.y * pixelSize);

            for (var x = cx - workerRadius; x <= cx + workerRadius; x++)
            {
                for (var y = cy - workerRadius; y <= cy + workerRadius; y++)
                {
                    texture.SetPixel(x, y, workerColor);
                }
            }
        });
        
        texture.Apply();
        Image.texture = texture;
    }
}
