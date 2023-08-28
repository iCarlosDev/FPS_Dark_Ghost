using System;
using UnityEngine;

public class ArmsSway : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    
    [Header("=== SWAY PARAMETERS ===")] 
    [Space(10)] 
    [SerializeField] private float _smooth;
    [SerializeField] private float _swayMultiplier;

    private void Awake()
    {
        _playerCore = GetComponentInParent<PlayerCore>();
    }

    private void Update()
    {
        Sway();
    }

    private void Sway()
    {
        float mouseX = _playerCore.PlayerIa.Camera.MouseX.ReadValue<float>() * _swayMultiplier;
        float mouseY = _playerCore.PlayerIa.Camera.MouseY.ReadValue<float>() * _swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _smooth * Time.deltaTime);
    }
}
