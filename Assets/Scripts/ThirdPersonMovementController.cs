using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ThirdPersonMovementController : MonoBehaviour
{


    const string ISGROUNDED = "IsGrounded", HORIZONTALSPEED = "HSpeed", VERTICALSPEED = "VSpeed", ISCROUCH = "IsCrouch";

    [Header("References")]
    /// <summary>
    /// the reference to the character controller of the character
    /// </summary>
    [SerializeField] CharacterController _controller;
    /// <summary>
    /// the aiming empty object for the camera looking purposes
    /// </summary>
    [SerializeField] Transform _lookAtTransform;
    /// <summary>
    /// reference to the AnimatorController of the character
    /// </summary>
    Animator _animator;
    /// <summary>
    /// keyboard and onscreen keybinding of the game
    /// </summary>
    InputActionAsset _keyBinding;
    /// <summary>
    /// this event invokes when ThirdPersonController inites in the game and passes the LookAt transform to the consumer
    /// </summary>
    public static event UnityAction<Transform> OnCharacterLoaded;
    [Header("Movement Parmeter")]
    [SerializeField] float _backFrontSpeed = 3f;
    [SerializeField] float _runSpeed = 5f;
    [SerializeField] float _sideSpeed = 3f;
    [SerializeField] float _rotationSpeed = 180f;
    [SerializeField] float _tiltSpeed = 180f;
    [SerializeField] float _tiltClamp = 15f;
    bool _isCrouch = false;
    bool _isRunning = false;
    float _xRotation = 0;
    float _yRotation = 0;
    [Header("Ground Check")]
    bool _isGrounded;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _groundCheckPosition;
    [SerializeField] float _groundCheckRadious=.1f;
    [SerializeField] float _groundCheckArea=.3f;
    [Header("Jump")]
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] float _freefallThreshold = .1f;
    Vector3 _velocity;
    float _groundVelocity = -2f;

    HealthPanelController _healthPanelController;
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClips;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (!_audioSource)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        OnCharacterLoaded?.Invoke(_lookAtTransform);
    }
    private void OnEnable()
    {
        _keyBinding?.Enable();
        HealthPanelController.OnHealthBarReady += HealthPanelController_OnHealthBarReady;
    }

    private void HealthPanelController_OnHealthBarReady(HealthPanelController iHealthBar)
    {
        _healthPanelController = iHealthBar;
    }

    private void OnDisable()
    {
        _keyBinding?.Disable();
        HealthPanelController.OnHealthBarReady -= HealthPanelController_OnHealthBarReady;
    }
    private void Update()
    {
        _groundCheck();

        _applyGravity();

        _animator.SetFloat(HORIZONTALSPEED, _controller.velocity.magnitude);
    }
    void _updateAnimator()
    {
        if (_animator != null)
        {
            _animator.SetBool(ISGROUNDED, _isGrounded);
            _animator.SetBool(ISCROUCH, _isCrouch);
            _animator.SetFloat(HORIZONTALSPEED, _controller.velocity.magnitude);
            Debug.Log(_controller.velocity+":" +_controller.velocity.magnitude);
            if (Mathf.Abs(_controller.velocity.y) > _freefallThreshold)
                _animator.SetFloat(VERTICALSPEED, _controller.velocity.y);
            else
                _animator.SetFloat(VERTICALSPEED, 0);


        }
    }
    void _applyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        
        if (Mathf.Abs(_controller.velocity.y) > _freefallThreshold)
            _animator.SetFloat(VERTICALSPEED, _controller.velocity.y);
        else
            _animator.SetFloat(VERTICALSPEED, 0);
    }
    public void _ToggleCrouch()
    {
        _isCrouch = !_isCrouch;

        _animator.SetBool(ISCROUCH, _isCrouch);
    }
    public void _ToggleRun()
    {
        _isRunning = !_isRunning;

    }
    public void _Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * _groundVelocity * _gravity);
        }


    }
    float _moveSpeed()
    {
        if (_isRunning) return _runSpeed;
        else return _backFrontSpeed;
    }
    void _groundCheck()
    {
        _isGrounded = Physics.CheckBox(_groundCheckPosition.position, new Vector3(_groundCheckArea, _groundCheckRadious, _groundCheckArea), Quaternion.identity, _groundLayer);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = _groundVelocity;
        }

        _animator.SetBool(ISGROUNDED, _isGrounded);

    }

    float _tilt(float iInputAmount)
    {
        float tiltAmount = iInputAmount * _tiltSpeed * Time.deltaTime;
        _xRotation -= tiltAmount;
        _xRotation = Mathf.Clamp(_xRotation, -_tiltClamp, _tiltClamp);
        return _xRotation;
    }
    public void _Move(Vector2 iMovementAmount)
    {
        Vector3 move = (transform.forward * iMovementAmount.y * _moveSpeed());

        _controller.Move(move * Time.deltaTime);

        _animator.SetFloat(HORIZONTALSPEED, _controller.velocity.magnitude);
        
        transform.Rotate(transform.up * Time.deltaTime * _rotationSpeed * iMovementAmount.x);
        
    }
    public void _Look(Vector2 iMovementAmount)
    {
        _yRotation += _rotationSpeed * Time.deltaTime * iMovementAmount.x;
        _lookAtTransform.localRotation = Quaternion.Euler(new Vector3(_tilt(iMovementAmount.y), _yRotation, 0));// +Quaternion.Euler(new Vector3(0,_lookAtTransform.rotation.y,_lookAtTransform.rotation.z));


    }
    public void _ResetLook()
    {
        _lookAtTransform.localRotation = Quaternion.identity;
    }

    public void _DoSpecial()
    {
        Debug.Log("REZZZAAA");
        _healthPanelController._PumpHealth();
        _audioSource.clip = _audioClips[Random.Range(0,_audioClips.Length)];
        _audioSource.Play();

    }


}
