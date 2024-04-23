using UnityEngine;
using UnityEngine.InputSystem;

public class FPSPlayerController : MonoBehaviour
{
    [Header("References")]
    CharacterController _ChController;
    [SerializeField] Transform _handAndCameraTrnsform;
    [Header("Movement Parmeter")]
    [SerializeField] float _backFrontSpeed;
    [SerializeField] float _sideSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _tiltSpeed;
    [SerializeField] float _tiltClamp;
    float _xRotation = 0;
    [Header("Ground Check")]
    bool _isGrounded;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _groundCheckPosition;
    [SerializeField] float _groundCheckRadious;
    [Header("Jump")]
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpHeight = 5f;
    Vector3 _velocity;
    float _groundVelocity = -2f;
    [Header("Key Bindings")]
    [SerializeField] InputActionAsset _keyBindings;

    private void Start()
    {
        _ChController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _groundCheck();
        _move();
        _rotate();
        _tilt();
        _jump();
    }
    void _jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * _groundVelocity * _gravity);
        }
        _velocity.y += _gravity * Time.deltaTime;
        _ChController.Move(_velocity * Time.deltaTime);
    }
    void _groundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadious);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = _groundVelocity;
        }
    }
    void _rotate()
    {
        float rotationAmount = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotationAmount, 0));
    }
    void _tilt()
    {
        float tiltAmount = Input.GetAxis("Mouse Y") * _tiltSpeed * Time.deltaTime;
        _xRotation -= tiltAmount;
        _xRotation = Mathf.Clamp(_xRotation, -_tiltClamp, _tiltClamp);
        _handAndCameraTrnsform.localRotation = Quaternion.Euler(new Vector3(_xRotation, 0, 0));
    }
    void _move()
    {
        float backFrontMovement = Input.GetAxis("Vertical");
        float sideMovement = Input.GetAxis("Horizontal");
        Vector3 move = (transform.forward * backFrontMovement * _backFrontSpeed) + (transform.right * sideMovement * _sideSpeed);
        _ChController.Move(move * Time.deltaTime);
    }
}
