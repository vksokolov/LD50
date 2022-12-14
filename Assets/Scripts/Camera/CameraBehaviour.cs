using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    private const int ZPosition = -1;
    private const float LeftPanelWidth = 4.78f;
    private const float CameraCenterOffset = LeftPanelWidth / 2f;
    private const int Height = 5;
    
    private Camera _camera;
    
    private Vector2 _minBound;
    private Vector2 _maxBound;
    private float _scrollSpeed;
    
    public void Setup(int mapHalfSize, float scrollSpeed)
    {
        _camera = GetComponent<Camera>();
        _scrollSpeed = scrollSpeed;
        
        float cellsPerScreenHeight = _camera.orthographicSize * 2;
        float cellsSizePixels = Screen.height / cellsPerScreenHeight;
        float cellsPerCameraHalfWidth = _camera.pixelWidth / cellsSizePixels / 2;
        float maxLeftOffset = cellsPerCameraHalfWidth - LeftPanelWidth;
        
        _minBound = new Vector2( -mapHalfSize + maxLeftOffset, Height - mapHalfSize);
        _maxBound = new Vector2(mapHalfSize - cellsPerCameraHalfWidth, mapHalfSize - Height);

        transform.position = new Vector3(int.MaxValue, int.MinValue);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;

        if (Input.GetKey(KeyCode.A))
            input += Vector2.left;

        if (Input.GetKey(KeyCode.S))
            input += Vector2.down;
        
        if (Input.GetKey(KeyCode.D))
            input += Vector2.right;

        input *= Time.deltaTime * _scrollSpeed;
        
        var currentPosition = transform.position;
        var targetPosition = new Vector3(
            Mathf.Clamp(currentPosition.x + input.x, _minBound.x, _maxBound.x),
            Mathf.Clamp(currentPosition.y + input.y, _minBound.y, _maxBound.y),
            ZPosition);
        transform.position = targetPosition;
    }
}
