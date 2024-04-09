using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private Transform _cameraTransform;

    private float _movementSpeed = 6f;
    private float _mouseSpeed = 5f;
    private float _jumpSpeed = 5f;
    private float _mass = 1f;
    
    
    private Vector3 _velocity;
    
    private Vector2 _look;

    private CharacterController _characterController;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * _mass * Time.deltaTime;
        _velocity.y = _characterController.isGrounded ? -1f : _velocity.y + gravity.y;
    }
    
    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();

        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _velocity.y += _jumpSpeed;
        }
        
        _characterController.Move((input * _movementSpeed + _velocity) * Time.deltaTime);
    }

    void UpdateLook()
    {
        _look.x += Input.GetAxis("Mouse X") * _mouseSpeed;
        _look.y += Input.GetAxis("Mouse Y") * _mouseSpeed;

        _look.y = Mathf.Clamp(_look.y, -89f, 89f);

        _cameraTransform.localRotation = Quaternion.Euler(-_look.y, 0, 0);
        // _weaponController.transform.rotation = Quaternion.Euler(-_look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, _look.x, 0);
    }
}
