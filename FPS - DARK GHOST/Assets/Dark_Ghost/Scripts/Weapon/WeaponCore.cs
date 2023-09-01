using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponCore : MonoBehaviour
{
    [SerializeField] private Weapon_GrabDropWeapon _weaponGrabDropWeapon;
    
    [Header("=== WEAPON COMPONENTS ===")]
    [Space(10)]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private List<Collider> _colliders;

    [Header("=== WEAPON SCRIPTABLE OBJECT ===")]
    [Space(10)]
    [SerializeField] private Weapon_ScriptableObject _weaponParameters;
    [SerializeField] private Transform _leftIkHand;
    [SerializeField] private Transform _rightIkHand;

    [Header("=== WEAPON EVENTS ===")]
    [Space(10)]
    [SerializeField] private UnityEvent<WeaponCore> _replaceWeaponEvent;

    //GETTERS && SETTERS//
    public Weapon_GrabDropWeapon WeaponGrabDropWeapon => _weaponGrabDropWeapon;
    public Rigidbody Rigidbody => _rigidbody;
    public List<Collider> Colliders => _colliders;
    public Weapon_ScriptableObject WeaponParameters => _weaponParameters;

    ////////////////////////////////////////////

    private void Awake()
    {
        _weaponGrabDropWeapon = GetComponent<Weapon_GrabDropWeapon>();
        
        _rigidbody = GetComponent<Rigidbody>();
        _colliders.AddRange(GetComponentsInChildren<Collider>());
        
    }

    private void Start()
    {
        _weaponParameters._rightHandPosition = _rightIkHand;
    }

    /// <summary>
    /// Método para llamar a un evento para reemplazar un arma agarrada por una del suelo;
    /// </summary>
    /// <param name="refWeapon"></param>
    public void ReplaceWeapon(WeaponCore refWeapon)
    {
        _replaceWeaponEvent.Invoke(refWeapon);
    }
}
