using UnityEngine;

public class KeepCameraInPlayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraPos;

    private void Update()
    {
        transform.position = _cameraPos.position;
    }
}
