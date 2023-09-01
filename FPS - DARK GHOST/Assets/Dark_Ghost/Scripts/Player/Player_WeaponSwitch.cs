using UnityEngine;

public class Player_WeaponSwitch : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    [SerializeField] private bool _isSwitchingWeapon;

    //GETTERS && SETTERS//
    public bool IsSwitchingWeapon => _isSwitchingWeapon;
    
    /////////////////////////////////////////////////////

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        if (_playerCore.PlayerIa.Actions.Switch_Up_Down.ReadValue<float>() > 0)
        {
            SwitchUp();
        }
        else if (_playerCore.PlayerIa.Actions.Switch_Up_Down.ReadValue<float>() < 0)
        {
            SwitchDown();
        }
    }

    /// <summary>
    /// Método para cambiar el arma actual por la última del inventario;
    /// </summary>
    private void SwitchUp()
    {
        if (_playerCore.PlayerInventory.InventoryWeaponList.Count == 1)
        {
            _playerCore.PlayerInventory.ReplaceCurrentWithInventoryWeapon(_playerCore.PlayerInventory.InventoryWeaponList.Count);
            return;
        }
        
        _playerCore.PlayerInventory.ReplaceCurrentWithInventoryWeapon(0);
    }

    /// <summary>
    /// Método para cambiar el arma actual por la primera del inventario;
    /// </summary>
    private void SwitchDown()
    {
        _playerCore.PlayerInventory.ReplaceCurrentWithInventoryWeapon(_playerCore.PlayerInventory.InventoryWeaponList.Count);
    }
}
