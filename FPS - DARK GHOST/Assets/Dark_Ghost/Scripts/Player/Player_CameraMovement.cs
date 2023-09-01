using UnityEngine;

public class Player_CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    
    [Header("=== CAMERA PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private Transform _playerBody;

    [Header("=== SENSITIVITY PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    [Header("=== CAMERA CLAMP ROTATION ===")]
    [Space(10)]
    [SerializeField] private float _maxVerticalCameraRotation;
    [SerializeField] private float _minVerticalCameraRotation;

    private float _xRotation;
    private float _yRotation;

    private void Awake()
    {
        _playerCore = FindObjectOfType<PlayerCore>();
        _playerBody = _playerCore.transform;
    }

    private void Update()
    {
        CameraControl();
    }

    /// <summary>
    /// Método que hace que podamos controlar la cámara con el mouse;
    /// </summary>
    private void CameraControl()
    {
        //Obtenemos el Input del Mouse;
        float mouseX = _playerCore.PlayerIa.Camera.MouseX.ReadValue<float>() * Time.deltaTime * _sensitivityX;
        float mouseY = _playerCore.PlayerIa.Camera.MouseY.ReadValue<float>() * Time.deltaTime * _sensitivityY;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minVerticalCameraRotation, _maxVerticalCameraRotation);
        
        //Rotamos la camara y el player;
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        _playerBody.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}
