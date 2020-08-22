using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, 0);
    [SerializeField] private Vector2 _panLimit = new Vector2(0, 0);
    [SerializeField] private float _panSpeed = 0f;
    [SerializeField] private float _panBorderThickness = 0f;
    [SerializeField] private float _scrollSpeed = 0f;
    [SerializeField] private float _minScale = 0f;
    [SerializeField] private float _maxScale = 0f;

    private void Start()
    {
        transform.position = _player.transform.position;
    }

    void Update()
    {
        Vector3 cameraPos = transform.position;

        float mousePosX = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width);
        float mousePosY = Mathf.Clamp(Input.mousePosition.y, 0, Screen.height);
        

        if(mousePosY >= Screen.height - _panBorderThickness)
        {
            cameraPos.y += _panSpeed * Time.deltaTime;
        }
        if (mousePosY <= _panBorderThickness)
        {
            cameraPos.y -= _panSpeed * Time.deltaTime;
        }
        if (mousePosX >= Screen.width - _panBorderThickness)
        {
            cameraPos.x += _panSpeed * Time.deltaTime;
        }
        if (mousePosX <= _panBorderThickness)
        {
            cameraPos.x -= _panSpeed * Time.deltaTime;
        }

        cameraPos.x = Mathf.Clamp(cameraPos.x, -_panLimit.x, _panLimit.x);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -_panLimit.y, _panLimit.y);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * 50f * _scrollSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _maxScale, _minScale);

        transform.position = cameraPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
    }

    void LateUpdate()
    {
        if (FindObjectOfType<PlayerMovement>()._isMoving)
        {
            transform.position = _player.transform.position + _offset;
        }
    }

    public void ResetCamera()
    {
        Camera.main.transform.position = _player.transform.position;
    }
}
