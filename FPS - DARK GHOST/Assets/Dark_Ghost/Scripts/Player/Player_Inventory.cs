using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    
    [Header("=== INVENTORY PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private WeaponCore _currentWeaponGrabbed;
    [SerializeField] private int _maxInventoryCapacity;
    
    private List<WeaponCore> _inventoryWeaponsList;

    //GETTERS && SETTERS//
    public WeaponCore CurrentWeaponGrabbed => _currentWeaponGrabbed;
    public List<WeaponCore> InventoryWeaponList => _inventoryWeaponsList;

    //////////////////////////////////////////////////////////

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Start()
    {
        _inventoryWeaponsList = new List<WeaponCore>();
    }

    /// <summary>
    /// Método para agarrar Armas;
    /// </summary>
    /// <param name="newWeapon"></param>
    public void GrabWeapon(WeaponCore newWeapon)
    {
        if (_inventoryWeaponsList.Count == _maxInventoryCapacity){ ReplaceCurrentWithGroundWeapon(newWeapon); return; }
        
        if (_currentWeaponGrabbed != null) AddWeaponToInventory(_currentWeaponGrabbed);
        
        AddWeaponToCurrentGrabbed(newWeapon);
    }

    /// <summary>
    /// Método para soltar armas;
    /// </summary>
    public void DropWeapon()
    {
        _currentWeaponGrabbed.WeaponGrabDropWeapon.DropWeaponByPlayer(_playerCore);
        RemoveWeaponFromCurrentGrabbed();

        if (_inventoryWeaponsList.Count == 0) return;
        AddWeaponToCurrentGrabbed(_inventoryWeaponsList[0]);
        RemoveWeaponFromInventory(0);
    }

    /// <summary>
    /// Método para añadir un arma a la actualmente agarrada;
    /// </summary>
    /// <param name="newWeapon"></param>
    private void AddWeaponToCurrentGrabbed(WeaponCore newWeapon)
    {
        _currentWeaponGrabbed = newWeapon;
        _currentWeaponGrabbed.gameObject.SetActive(true);
        
        _playerCore.PlayerAnimations.SetPlayerArmed();
    }

    /// <summary>
    /// Método para quitar un arma de la actualmente agarrada;
    /// </summary>
    private void RemoveWeaponFromCurrentGrabbed()
    {
        _currentWeaponGrabbed = null;
        
        _playerCore.PlayerAnimations.SetPlayerUnarmed();
    }

    /// <summary>
    /// Método para añadir un arma al inventario;
    /// </summary>
    /// <param name="currentWeapon"></param>
    private void AddWeaponToInventory(WeaponCore currentWeapon)
    {
        currentWeapon.gameObject.SetActive(false);
        
        _inventoryWeaponsList.Add(currentWeapon);
        _inventoryWeaponsList.Add(_inventoryWeaponsList[0]);
        _inventoryWeaponsList.RemoveAt(0);
    }

    /// <summary>
    /// Método para quitar un arma del inventario;
    /// </summary>
    /// <param name="inventoryWeaponStackIndex"></param>
    private void RemoveWeaponFromInventory(int inventoryWeaponStackIndex)
    {
        _inventoryWeaponsList.RemoveAt(inventoryWeaponStackIndex);
    }

    /// <summary>
    /// Método para reemplazar un arma agarrada por otra del suelo;
    /// </summary>
    /// <param name="newWeapon"></param>
    private void ReplaceCurrentWithGroundWeapon(WeaponCore newWeapon)
    {
        _currentWeaponGrabbed.ReplaceWeapon(newWeapon);
        AddWeaponToCurrentGrabbed(newWeapon);
    }

    /// <summary>
    /// Método para reemplazar un arma del inventario por la que tengamos agarrada;
    /// </summary>
    /// <param name="inventoryWeaponStackIndex"></param>
    public void ReplaceCurrentWithInventoryWeapon(int inventoryWeaponStackIndex)
    {
        if (_inventoryWeaponsList.Count == 0) return;

        AddWeaponToInventory(_playerCore.PlayerInventory.CurrentWeaponGrabbed);
        AddWeaponToCurrentGrabbed(_playerCore.PlayerInventory.InventoryWeaponList[inventoryWeaponStackIndex]);
        RemoveWeaponFromInventory(inventoryWeaponStackIndex);
    }
}
