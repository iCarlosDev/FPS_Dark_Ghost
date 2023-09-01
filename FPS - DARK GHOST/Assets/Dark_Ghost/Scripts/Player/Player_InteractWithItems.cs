using UnityEngine;

public class Player_InteractWithItems : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    
    [Header("=== INTERACT PARAMETERS ===")]
    [Space(10)]
    [SerializeField] private LayerMask _interactableItemLayer;
    [SerializeField] private float _rayDistance;

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        if (_playerCore.PlayerIa.Actions.Interact.triggered)
        {
            Interact();
        }
    }

    /// <summary>
    /// Método para interactuar con cualquier objeto que tenga la layer adecuada;
    /// </summary>
    private void Interact()
    {
        Ray ray = new Ray(_playerCore.PlayerCamera.position, _playerCore.PlayerCamera.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, _rayDistance, _interactableItemLayer))
        {
            if (hit.transform.TryGetComponent(out I_Interact interactableItem))
            {
                interactableItem.Interact(_playerCore);
            }
        }
    }
}
