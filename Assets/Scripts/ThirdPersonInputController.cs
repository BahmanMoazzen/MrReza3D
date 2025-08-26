using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonInputController : MonoBehaviour
{
    /// <summary>
    /// the reference to the movement controller in order to issue movement commands
    /// </summary>
    [SerializeField] ThirdPersonMovementController _movementController;
    /// <summary>
    /// minimum movement to invoke player to move
    /// </summary>
    [SerializeField] float _movementDeadzone = .01f;
    /// <summary>
    /// the speed to transfere between values in joystick
    /// </summary>
    [SerializeField] float _joystickIntensity = 4f;

    /// <summary>
    /// keyboard and input bindings
    /// </summary>
    KeyBindings _keyBinding;
    /// <summary>
    /// move passthrough
    /// </summary>
    InputAction _move;
    /// <summary>
    /// look passthrough
    /// </summary>
    InputAction _look;
    /// <summary>
    /// the current movement to learp to
    /// </summary>
    Vector2 _currentMovement;


    private void Awake()
    {
        _keyBinding = new KeyBindings();

    }
    private void OnEnable()
    {
        _keyBinding.Player.Crouch.performed += Crouch_performed;
        _keyBinding.Player.Run.performed += Run_performed;
        _keyBinding.Player.Jump.started += Jump_started;
        _keyBinding.Player.SpecialMove.performed += Reza_performed;
        _keyBinding.Player.ResetLook.started += ResetLook_started;
        _move = _keyBinding.Player.Movement;
        _look = _keyBinding.Player.Look;
        _keyBinding.Player.Enable();
    }

    private void ResetLook_started(InputAction.CallbackContext obj)
    {
        _movementController._ResetLook();
    }

    private void Reza_performed(InputAction.CallbackContext obj)
    {
        AnalyticsManager._RezaCalled();
        _movementController._DoSpecial();
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        _movementController._Jump();
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        _movementController._ToggleRun();
    }

    private void Crouch_performed(InputAction.CallbackContext obj)
    {
        _movementController._ToggleCrouch();
    }

    private void OnDisable()
    {
        _keyBinding.Player.Crouch.performed -= Crouch_performed;
        _keyBinding.Player.Run.performed -= Run_performed;
        _keyBinding.Player.Jump.started -= Jump_started;
        _keyBinding.Player.SpecialMove.performed -= Reza_performed;
        _keyBinding.Player.ResetLook.started -= ResetLook_started;
        _keyBinding.Player.Disable();
    }


    void Update()
    {

        Vector2 move = _move.ReadValue<Vector2>();
        Vector2 look = _look.ReadValue<Vector2>();
        if (move.magnitude > _movementDeadzone)
        {


            move.x = Mathf.MoveTowards(_currentMovement.x, move.x, _joystickIntensity * Time.deltaTime);
            move.y = Mathf.MoveTowards(_currentMovement.y, move.y, _joystickIntensity * Time.deltaTime);
            _currentMovement = move;
            _movementController._Move(_currentMovement);
            //_movementController._Move((Camera.main.transform.forward * move.y) + (Camera.main.transform.right * move.x));
        }
        if (look.magnitude > _movementDeadzone)
        {
            _movementController._Look(look);
        }
    }
}
