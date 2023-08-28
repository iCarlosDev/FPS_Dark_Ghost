using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("=== COMPONENTS ===")]
    [SerializeField] private PlayerCore _playerCore;

    [Header("=== MOVEMENT PARAMETERS ===")] 
    [Space(10)]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _crouchSpeed;
    private float _currentSpeed;

    [Header("=== JUMP PARAMETERS ===")] 
    [Space(10)] 
    [SerializeField] private float _jumpHeight;

    [Header("=== CROUCH PARAMETERS ===")] 
    [Space(10)]
    [SerializeField] private bool _isCrouching;
    [SerializeField] private float _crouchHeight;
    private float _standHeight;

    [Header("=== SLIDE PARAMETERS ===")] 
    [Space(10)] 
    [SerializeField] private bool _isSliding;
    [SerializeField] private float _slideHeight;
    [SerializeField] private float _maxSlideTime;
    private float _slideTimer;
    [SerializeField] private float _slideForce;

    [Header("=== GROUND CHECK ===")] 
    [Space(10)] 
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkGroundDistance;
    [SerializeField] private bool _isGrounded;
    
    [Header("=== GRAVITY PARAMETERS ===")] 
    [Space(10)] 
    [SerializeField] private float _gravity;
    private Vector3 velocity;

    [Header("=== PLAYER STATE ===")] 
    [Space(10)] 
    [SerializeField] private PlayerState _playerStateEnum;

    public enum PlayerState
    {
        Idle,
        Walking,
        Sprinting,
        Crouching,
        Sliding,
        InAir
    }

    private float _verticalInput;
    private float _horizontalInput;

    //GETTERS && SETTERS//
    public float StandHeight => _standHeight;
    public PlayerState PlayerStateEnum => _playerStateEnum;
    
    ////////////////////////////////////////////////////////

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Start()
    {
        _standHeight = _playerCore.CharacterController.height;
    }

    private void Update()
    {
        CheckGround();
        SetSpeed();
        UseGravity();
        
        MovePlayer();
        Jump();
        
        if ((_playerCore.PlayerIa.Movement.Jump.triggered || _playerCore.PlayerIa.Movement.Crouch.triggered) && _isSliding) StopSliding();
        if (_playerCore.PlayerIa.Movement.Crouch.triggered && _playerStateEnum == PlayerState.Sprinting) StartSlide();
        if (_isSliding) SlidingMovement();
        
        if (_playerCore.PlayerIa.Movement.Crouch.triggered) SetPlayerPosition();
    }

    #region - ACTIONS -
    
    private void MovePlayer()
    {
        _horizontalInput = _playerCore.PlayerIa.Movement.WASD.ReadValue<Vector2>().x;
        _verticalInput = _playerCore.PlayerIa.Movement.WASD.ReadValue<Vector2>().y;

        Vector3 direction = transform.forward * _verticalInput + transform.right * _horizontalInput;

        _playerCore.CharacterController.Move(direction.normalized * Time.deltaTime * _currentSpeed);
    }

    private void Jump()
    {
        if (!_isGrounded || !_playerCore.PlayerIa.Movement.Jump.triggered) return;
        
        if (_isCrouching)
        {
            Stand();
            return;
        }
            
        velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }

    private void Stand()
    {
        _playerCore.CharacterController.height = _standHeight;
        _isCrouching = false;
    }

    private void Crouch()
    {
        _playerCore.CharacterController.height = _crouchHeight;
        _isCrouching = true;
    }

    private void StartSlide()
    {
        _isSliding = true;
        _playerCore.CharacterController.height = _slideHeight;
        _slideTimer = _maxSlideTime;
    }
    
    private void StopSliding()
    {
        _isSliding = false;
        if (_slideTimer <= 0)
        {
            Crouch();
        }
        else
        {
            Stand();
        }
    }
    
    private void SlidingMovement()
    {
        Vector3 inputDirection = transform.forward * _verticalInput + transform.right * _horizontalInput;
        _playerCore.CharacterController.Move(inputDirection.normalized * _slideForce * Time.deltaTime);

        _slideTimer -= Time.deltaTime;

        if (_slideTimer <= 0f) StopSliding();
    }
    #endregion

    #region - CALCULATIONS -

    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _checkGroundDistance, _groundMask);
    }

    private void SetSpeed()
    {
        switch (_isGrounded)
        {
            case true when _isSliding:
                _playerStateEnum = PlayerState.Sliding;
                break;
            case true when _isCrouching:
                _playerStateEnum = PlayerState.Crouching;
                _currentSpeed = _crouchSpeed;
                break;
            case true when _playerCore.CharacterController.velocity.magnitude > 0.1f && _playerCore.PlayerIa.Movement.Sprint.IsPressed():
                _playerStateEnum = PlayerState.Sprinting;
                _currentSpeed = _sprintSpeed;
                break;
            case true when _playerCore.CharacterController.velocity.magnitude > 0.1f:
                _playerStateEnum = PlayerState.Walking;
                _currentSpeed = _walkSpeed;
                break;
            case true when _playerCore.CharacterController.velocity.magnitude < 0.1f:
                _playerStateEnum = PlayerState.Idle;
                _currentSpeed = _walkSpeed;
                break;
            case false:
                _playerStateEnum = PlayerState.InAir;
                break;
        }
    }
    
    private void SetPlayerPosition()
    {
        switch (_isGrounded)
        {
            case true when !_isCrouching: Crouch(); break;
            case true when _isCrouching: Stand(); break;
        }
    }
    
    private void UseGravity()
    {
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += _gravity * Time.deltaTime;
        _playerCore.CharacterController.Move(velocity * Time.deltaTime);
    }

    #endregion
}
