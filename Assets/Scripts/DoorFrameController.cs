using UnityEngine;

public class DoorFrameController : MonoBehaviour
{
    OpenCloseDoor _theDoor;
    void Start()
    {
        _theDoor = GetComponentInChildren<OpenCloseDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _theDoor != null)
            _theDoor._OpenDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _theDoor != null)
            _theDoor._CloseDoor();
    }
}
