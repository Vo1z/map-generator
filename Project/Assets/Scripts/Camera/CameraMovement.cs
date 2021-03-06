/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: ROB1N (Ivan Fomenko)
 * Email: iv_fomenko@bk.ru
 * Twitter: @ROB1N21806
 *
 * Extended by
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_ 
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [Header("Border control options")]
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, 0);
    [SerializeField] private Vector2 _panLimit = new Vector2(0, 0);
    [SerializeField] private float _panSpeed = 0f;
    [SerializeField] private float _panBorderThickness = 0f;
    [SerializeField] private float _scrollSpeed = 0f;
    [SerializeField] private float _minScale = 0f;
    [SerializeField] private float _maxScale = 0f;

    [Header("Drug control options")]
    [SerializeField] private float dragSpeed = 15;
    //Variable that is responsible for holding button that moves camera during drag camera movement
    [SerializeField] private KeyCode dragButton = KeyCode.Mouse2;
    
    //Variable that is used to track mouse position on the previous frame for drag camera movement
    private Vector3 _oldMousePosition;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _oldMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if(Input.GetKey(dragButton))
            DrugCameraControl();
        else
            BorderMovingCameraControl();
    }

    private void DrugCameraControl()
    {
        if (Input.GetKeyDown(dragButton))
        {
            _oldMousePosition = Input.mousePosition;
            return;
        }
        if (!Input.GetKey(dragButton) && _oldMousePosition == Input.mousePosition) 
            return;
        
        Vector2 pos = _mainCamera.ScreenToViewportPoint(_oldMousePosition - Input.mousePosition);
        Vector2 move = new Vector2(pos.x * dragSpeed, pos.y * dragSpeed);

        transform.Translate(move, Space.World);
        _oldMousePosition = Input.mousePosition;
    }
    
    private void BorderMovingCameraControl()
    {
        Vector3 cameraPos = transform.position;
        
        float mousePosX = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width);
        float mousePosY = Mathf.Clamp(Input.mousePosition.y, 0, Screen.height);

        if(mousePosY >= Screen.height - _panBorderThickness) 
            cameraPos.y += _panSpeed * Time.deltaTime;
        if (mousePosY <= _panBorderThickness) 
            cameraPos.y -= _panSpeed * Time.deltaTime;
        if (mousePosX >= Screen.width - _panBorderThickness)
            cameraPos.x += _panSpeed * Time.deltaTime;
        if (mousePosX <= _panBorderThickness)
            cameraPos.x -= _panSpeed * Time.deltaTime;
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _mainCamera.orthographicSize -= scroll * 50f * _scrollSpeed * Time.deltaTime;
        _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize, _maxScale, _minScale);
        
        transform.position = cameraPos;
    }
}
