using System;
using UnityEngine;

public class Player_DropItems : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        if (_playerCore.PlayerIa.Actions.Drop.triggered)
        {
            DropWeapons();
        }
    }

    /// <summary>
    /// Método para soltar armas;
    /// </summary>
    private void DropWeapons()
    {
        if (_playerCore.PlayerInventory.CurrentWeaponGrabbed == null) return;

        _playerCore.PlayerInventory.DropWeapon();
    }
}
