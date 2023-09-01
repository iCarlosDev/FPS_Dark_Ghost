using UnityEngine;

public class Weapon_GrabDropWeapon : MonoBehaviour, I_Interact
{
    [SerializeField] private WeaponCore _weaponCore;

    [Header("=== WEAPON POSITION IN HAND ===")]
    [Space(10)]
    [SerializeField] private Vector3 _weaponPositionOffset;

    [Header("=== WEAPON DROP PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private float _dropForce;
    [SerializeField] private float _dropTorqueForce;

    private void Awake()
    {
        _weaponCore = GetComponent<WeaponCore>();
    }

    /// <summary>
    /// Método que se llama cuando el player interactua con el arma;
    /// </summary>
    /// <param name="playerCore"></param>
    public void Interact(PlayerCore playerCore)
    {
        GrabWeaponByPlayer(playerCore);
    }
    
    /// <summary>
    /// Método para habilitar el rigidbody del arma;
    /// </summary>
    private void EnableRigidbody()
    {
        _weaponCore.Rigidbody.isKinematic = false;
    }
    
    /// <summary>
    /// Método para deshabilitar el rigidbody del arma;
    /// </summary>
    private void DisableRigidbody()
    {
        _weaponCore.Rigidbody.isKinematic = true;
    }

    /// <summary>
    /// Método para habilitar los colliders del arma;
    /// </summary>
    private void EnableColliders()
    {
        foreach (var collider in _weaponCore.Colliders)
        {
            collider.enabled = true;
        }
    }

    /// <summary>
    /// Método para deshabilitar los colliders del arma;
    /// </summary>
    private void DisableColliders()
    {
        foreach (var collider in _weaponCore.Colliders)
        {
            collider.enabled = false;
        }
    }

    /// <summary>
    /// Método para cambiar ajustes de componentes del arma para poder agarrarla;
    /// </summary>
    /// <param name="playerCore"></param>
    private void GrabWeaponByPlayer(PlayerCore playerCore)
    {
        playerCore.WeaponGrabbed(_weaponCore);
        
        DisableRigidbody();
        DisableColliders();
        transform.parent = playerCore.WeaponSocket;
        transform.localPosition = Vector3.zero + _weaponPositionOffset;
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }

    /// <summary>
    /// Método para cambiar ajustes de componentes del arma para poder soltarlo;
    /// </summary>
    /// <param name="playerCore"></param>
    public void DropWeaponByPlayer(PlayerCore playerCore)
    {
        EnableRigidbody();
        EnableColliders();
        transform.parent = null;
        
        _weaponCore.Rigidbody.AddForce(playerCore.PlayerCamera.forward * _dropForce, ForceMode.Impulse);
        _weaponCore.Rigidbody.AddTorque(playerCore.PlayerCamera.right * _dropTorqueForce, ForceMode.Impulse);
        _weaponCore.Rigidbody.AddTorque(playerCore.PlayerCamera.up * _dropTorqueForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Método para dejar un arma en la misma posición que otra en el suelo;
    /// </summary>
    /// <param name="refWeapon"></param>
    public void LeaveWeaponOnOtherWeaponPlace(WeaponCore refWeapon)
    {
        transform.parent = null;

        transform.position = refWeapon.transform.position;
        transform.rotation = refWeapon.transform.rotation;
        
        EnableColliders();
        EnableRigidbody();
    }
}
