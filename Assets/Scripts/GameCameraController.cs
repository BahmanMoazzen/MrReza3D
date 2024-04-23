using UnityEngine;
using Cinemachine;

public class GameCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    private void OnEnable()
    {
        ThirdPersonMovementController.OnCharacterLoaded += ThirdPersonMovementController_OnCharacterLoaded;
    }

    private void ThirdPersonMovementController_OnCharacterLoaded(Transform iLookAt)
    {
        _cam.Follow = iLookAt;
        _cam.LookAt = iLookAt;
    }

    private void OnDisable()
    {
        ThirdPersonMovementController.OnCharacterLoaded -= ThirdPersonMovementController_OnCharacterLoaded;
    }

}
