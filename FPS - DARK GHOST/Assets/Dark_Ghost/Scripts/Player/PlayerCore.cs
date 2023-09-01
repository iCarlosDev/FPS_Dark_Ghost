using UnityEngine;
using UnityEngine.Events;

public class PlayerCore : MonoBehaviour
{
    public Player_IA PlayerIa { get; private set; }
    
    [Header("=== PLAYER SCRIPTS ===")]
    [Space(10)]
    [SerializeField] private Player_Inventory _playerInventory;
    [SerializeField] private Player_Animations _playerAnimations;
    
    [Header("=== PLAYER COMPONENTS ===")]
    [Space(10)]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _weaponSocket;
    [SerializeField] private Transform _playerCamera;

    [Header("=== PLAYER EVENTS ===")]
    [Space(10)]
    [SerializeField] private UnityEvent<WeaponCore> _grabWeapon;

    //GETTERS && SETTERS//
    public Player_Inventory PlayerInventory => _playerInventory;
    public Player_Animations PlayerAnimations => _playerAnimations;
    public CharacterController CharacterController => _characterController;
    public Transform WeaponSocket => _weaponSocket;
    public Transform PlayerCamera => _playerCamera;

    /////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        PlayerIa = new Player_IA();
        _playerInventory = GetComponent<Player_Inventory>();
        _playerAnimations = GetComponent<Player_Animations>();
        _characterController = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        PlayerIa.Enable();
    }

    private void OnDisable()
    {
        PlayerIa.Disable();
    }

    /// <summary>
    /// Método para llamar a un evento que agarra un arma del suelo;
    /// </summary>
    /// <param name="weapon"></param>
    public void WeaponGrabbed(WeaponCore weapon)
    {
        _grabWeapon.Invoke(weapon);
    }
}
