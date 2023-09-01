using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Weapon Scriptable Object",fileName = "WeaponName_ScriptableObject")]
public class Weapon_ScriptableObject : ScriptableObject
{
    [Header("=== LEFT IK HAND PARAMETERS ===")] 
    [Space(10)] 
    public Vector3 _leftHandPosition;
    public Vector3 _leftHandRotation;

    [Header("=== RIGHT IK HAND PARAMETERS ===")] 
    [Space(10)] 
    public Transform _rightHandPosition;

    [Header("=== WEAPON AMMO PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private int _maxAmmoClipCapacity;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmoClip;
    
    [Header("=== WEAPON STATUS ===")]
    [Space(10)]
    [SerializeField] private bool _isSemiautomatic;
}
