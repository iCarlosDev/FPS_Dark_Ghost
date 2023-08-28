using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public Player_IA PlayerIa { get; private set; }
    
    [SerializeField] private CharacterController _characterController ;

    //GETTERS && SETTERS//
    public CharacterController CharacterController => _characterController;
    
    /////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        PlayerIa = new Player_IA();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        PlayerIa.Enable();
    }

    private void OnDisable()
    {
        PlayerIa.Disable();
    }
}
