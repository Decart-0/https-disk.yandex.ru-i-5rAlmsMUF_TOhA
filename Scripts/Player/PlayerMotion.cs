using System;
using UnityEngine;

[RequireComponent(typeof(DetectorGround))]
[RequireComponent(typeof(InputScheme))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerFlipper))]
public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _isOnGround;

    private InputScheme _inputScheme;
    private Rigidbody2D _rigidbody;
    private DetectorGround _detectorGround;
    private PlayerFlipper _playerFlipper;

    public event Action Moving;

    public float Direction { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputScheme = GetComponent<InputScheme>();
        _detectorGround = GetComponent<DetectorGround>();
        _playerFlipper = GetComponent<PlayerFlipper>();
        _isOnGround = true;
    }

    private void OnEnable()
    {
        _detectorGround.GroundStatusChanged += UpdateIsGround;
    }

    private void Update()
    {
        Move();
        Moving?.Invoke();

        if (Input.GetKeyDown(_inputScheme.Jump) && _isOnGround)
        {
            Jump();
        }
    }

    private void OnDisable()
    {
        _detectorGround.GroundStatusChanged -= UpdateIsGround;
    }

    public void UpdateIsGround()
    {
        _isOnGround = _detectorGround.IsOnGround;
    }

    private void Move()
    {
        Direction = Input.GetAxis(_inputScheme.AxisHorizontal);

        if (Direction != 0)
        {
            _rigidbody.velocity = new Vector2(Direction * _speed, _rigidbody.velocity.y);
            _playerFlipper.Flip(Direction);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}