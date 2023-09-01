using UnityEngine;

public class Player_ArmsPose : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;
    
    [SerializeField] private Transform _leftIKHand;
    [SerializeField] private Transform _rightIKHand;

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        SetIKHandsPose();
    }

    private void SetIKHandsPose()
    {
        if (!_playerCore.PlayerInventory.CurrentWeaponGrabbed) return;
        if (!_playerCore.PlayerInventory.CurrentWeaponGrabbed.TryGetComponent(out WeaponCore weapon)) return;
       
        SetLeftHandPose(weapon.WeaponParameters._leftHandPosition, weapon.WeaponParameters._leftHandRotation);
        SetRightHandPose(weapon.WeaponParameters._rightHandPosition);
    }

    private void SetLeftHandPose(Vector3 leftHandPosition, Vector3 leftHandRotation)
    {
        _leftIKHand.transform.localPosition = leftHandPosition;
        _leftIKHand.transform.localRotation = Quaternion.Euler(leftHandRotation);
    }

    private void SetRightHandPose(Transform handPosition)
    {
        _rightIKHand.transform.position = handPosition.position;
        _rightIKHand.transform.rotation = Quaternion.Euler(handPosition.eulerAngles);
    }
}
